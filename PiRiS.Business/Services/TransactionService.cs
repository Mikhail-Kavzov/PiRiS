using AutoMapper;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;
using PiRiS.Data.Models.Enums;
using PiRiS.Business.Exceptions;
using PiRiS.Common.Constants;
using Microsoft.Extensions.Options;
using PiRiS.Business.Options;

namespace PiRiS.Business.Services;

public class TransactionService : BaseService, ITransactionService
{
    private readonly IAccountService _accountService;
    private readonly IBankService _bankService;
    private readonly CurrencyOptions _currencyOptions;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService,
        IBankService bankService, IOptions<CurrencyOptions> currencyOptions) : base(unitOfWork, mapper)
    {
        _accountService = accountService;
        _bankService = bankService;
        _currencyOptions = currencyOptions.Value;
    }

    public async Task CloseCreditsForDayAsync()
    {
        var currentDay = await _bankService.GetCurrentDayAsync();

        var credits = await UnitOfWork.CreditRepository
            .GetListAsync(0, int.MaxValue, x => x.Sum > 0 && currentDay >= x.StartDate && currentDay <= x.EndDate);

        foreach (var credit in credits)
        {
            decimal percentSum = 0;
            var monthes = credit.CreditPlan.MonthPeriod;

            double monthPercent = credit.CreditPlan.Percent / BankParams.MonthInYear / BankParams.PercentDelimiter;

            if (credit.CreditPlan.CreditType == CreditType.Annuity)
            {
                var temp = Math.Pow(1 + monthPercent, monthes);
                var coef = monthPercent * temp / (temp - 1);

                var monthPayment = coef * (double)credit.Sum;
                percentSum = (decimal)monthPayment / BankParams.DaysInMonth;
            }
            else
            {
                var rest = (double)credit.Sum;
                var dayCreditPart = (double)credit.Sum / monthes / BankParams.DaysInMonth;
                double percentPerDay = monthPercent / BankParams.DaysInMonth;

                var totalDays = (currentDay - credit.StartDate).TotalDays;
                for (int i = 0; i < totalDays; i++)
                {
                    rest -= dayCreditPart;
                }

                percentSum = (decimal)(dayCreditPart + rest * percentPerDay);
            }

            var currencyName = credit.CreditPlan.Currency.CurrencyName;
            var exchangeRate = _currencyOptions.ExchangeCourse[currencyName];
            var fundAccount = await _accountService.GetFundAccountAsync();
            await PerformTransactionAsync(credit.PercentAccount, fundAccount, percentSum * exchangeRate);
        }
    }

    public async Task CloseDepositsForDayAsync()
    {
        var currentDay = await _bankService.GetCurrentDayAsync();
        var deposits = await UnitOfWork.DepositRepository
            .GetListAsync(0, int.MaxValue, x => x.Sum > 0 && currentDay >= x.StartDate && currentDay <= x.EndDate);

        foreach (var deposit in deposits)
        {
            var percentSum = deposit.Sum * (decimal)(deposit.DepositPlan.Percent / BankParams.PercentDelimiter / BankParams.DaysInYear);
            var fundAccount = await _accountService.GetFundAccountAsync();

            var currencyName = deposit.DepositPlan.Currency.CurrencyName;
            var exchangeRate = _currencyOptions.ExchangeCourse[currencyName];

            await PerformTransactionAsync(fundAccount, deposit.PercentAccount, percentSum * exchangeRate);
        }
    }

    public async Task PerformBankDebitTransactionAsync(decimal amount)
    {
        UnitOfWork.AccountRepository.ClearContext();
        var bankAccount = await _accountService.GetBankAccountAsync();
        bankAccount.Debit += amount;
        bankAccount.Balance = bankAccount.Debit - bankAccount.Credit;
        UnitOfWork.AccountRepository.Update(bankAccount);
        await UnitOfWork.AccountRepository.SaveChangesAsync();
    }

    public async Task PerformTransactionAsync(int debitAccountId, int creditAccountId, decimal amount)
    {
        var debitAccount = await UnitOfWork.AccountRepository.GetEntityAsync(debitAccountId);
        var creditAccount = await UnitOfWork.AccountRepository.GetEntityAsync(creditAccountId);

        if (debitAccount == null)
        {
            throw new NotFoundException("Debit account not found");
        }

        if (creditAccount == null)
        {
            throw new NotFoundException("Credit account not found");
        }

        await PerformTransactionAsync(debitAccount, creditAccount, amount);
    }

    public async Task PerformTransactionAsync(Account debitAccount, Account creditAccount, decimal amount)
    {
        UnitOfWork.AccountRepository.ClearContext();
        if (debitAccount.AccountPlan.AccountType == AccountType.Passive)
        {
            debitAccount.Debit += amount;
            debitAccount.Balance = debitAccount.Credit - debitAccount.Debit;
        }
        else
        {
            debitAccount.Credit += amount;
            debitAccount.Balance = debitAccount.Debit - debitAccount.Credit;
        }

        if (creditAccount.AccountPlan.AccountType == AccountType.Passive)
        {
            creditAccount.Credit += amount;
            creditAccount.Balance = creditAccount.Credit - creditAccount.Debit;
        }
        else
        {
            creditAccount.Debit += amount;
            creditAccount.Balance = creditAccount.Debit - creditAccount.Credit;
        }

        var currentDay = await _bankService.GetCurrentDayAsync();
        var transaction = new Transaction
        {
            DebitAccountId = debitAccount.AccountId,
            CreditAccountId = creditAccount.AccountId,
            Amount = amount,
            TransactionDay = currentDay,
        };

        UnitOfWork.TransactionRepository.Create(transaction);
        debitAccount.AccountPlan = null;
        UnitOfWork.AccountRepository.Update(debitAccount);
        creditAccount.AccountPlan = null;
        UnitOfWork.AccountRepository.Update(creditAccount);

        await UnitOfWork.AccountRepository.SaveChangesAsync();
        await UnitOfWork.TransactionRepository.SaveChangesAsync();
    }

    public async Task WithdrawBankTransactionAsync(decimal amount)
    {
        UnitOfWork.AccountRepository.ClearContext();
        var bankAccount = await _accountService.GetBankAccountAsync();
        bankAccount.Credit += amount;
        bankAccount.Balance = bankAccount.Debit - bankAccount.Credit;
        UnitOfWork.AccountRepository.Update(bankAccount);
        await UnitOfWork.AccountRepository.SaveChangesAsync();
    }
}
