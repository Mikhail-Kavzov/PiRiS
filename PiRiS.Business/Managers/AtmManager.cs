using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PiRiS.Business.Dto.Account;
using PiRiS.Business.Dto.Atm;
using PiRiS.Business.Dto.Credit;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Options;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Managers;

public class AtmManager : BaseManager, IAtmManager
{
    private readonly ITransactionService _transactionService;
    private readonly IAccountService _accountService;
    private readonly CurrencyOptions _currencyOptions;

    public AtmManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger<AtmManager> logger,
        ITransactionService transactionService, IAccountService accountService, IOptions<CurrencyOptions> currencyOptions)
        : base(mapper, unitOfWork, logger)
    {
        _transactionService = transactionService;
        _accountService = accountService;
        _currencyOptions = currencyOptions.Value;
    }

    public async Task<AccountDto> GetAccountAsync(string accountNumber)
    {
        var account = await UnitOfWork.AccountRepository.GetEntityAsync(x => x.AccountNumber == accountNumber);

        if (account == null)
        {
            throw new NotFoundException("Account not found");
        }

        return Mapper.Map<AccountDto>(account);
    }

    public async Task<CreditDto> LoginAsync(string creditCardNumber, string creditCardCode)
    {
        var credit = await UnitOfWork.CreditRepository
            .GetEntityAsync(x=> x.CreditCardNumber ==  creditCardNumber && x.CreditCardCode == creditCardCode);

        if (credit == null)
        {
            throw new ServiceException("Enter valid card credentials");
        }

        return Mapper.Map<CreditDto>(credit);
    }

    public async Task WithdrawMoneyAsync(int creditId, decimal sum)
    {
        var credit = await UnitOfWork.CreditRepository.GetEntityAsync(creditId);
        if (credit.MainAccount.Balance < sum)
        {
            throw new ServiceException("Account doesn't have enough money");
        }

        var bankAccount = await _accountService.GetBankAccountAsync();
        var currencyName = credit.CreditPlan.Currency.CurrencyName;

        var exchageRate = _currencyOptions.ExchangeCourse[currencyName];

        var sumInByn = sum * exchageRate;

        await _transactionService.PerformTransactionAsync(credit.MainAccount, bankAccount, sumInByn);

        await _transactionService.WithdrawBankTransactionAsync(sumInByn);
    }
}
