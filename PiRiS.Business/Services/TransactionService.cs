using AutoMapper;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;
using PiRiS.Data.Models.Enums;
using PiRiS.Business.Exceptions;

namespace PiRiS.Business.Services;

public class TransactionService : BaseService, ITransactionService
{
    private readonly IAccountService _accountService;
    private readonly IBankService _bankService;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService, IBankService bankService) : base(unitOfWork, mapper)
    {
        _accountService = accountService;
        _bankService = bankService;
    }

    public async Task PerformBankDebitTransactionAsync(decimal amount)
    {
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
            DebitAccount = debitAccount,
            CreditAccount = creditAccount,
            Amount = amount,
            TransactionDay = currentDay,
        };

        UnitOfWork.TransactionRepository.Create(transaction);
        await UnitOfWork.TransactionRepository.SaveChangesAsync();
    }

    public async Task WithdrawBankTransactionAsync(decimal amount)
    {
        var bankAccount = await _accountService.GetBankAccountAsync();
        bankAccount.Credit += amount;
        bankAccount.Balance = bankAccount.Debit - bankAccount.Credit;
        UnitOfWork.AccountRepository.Update(bankAccount);
        await UnitOfWork.AccountRepository.SaveChangesAsync();
    }
}
