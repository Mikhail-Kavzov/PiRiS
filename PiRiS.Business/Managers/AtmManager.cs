using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PiRiS.Business.Dto.Account;
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

        await _transactionService.PerformTransactionAsync(credit.MainAccount, bankAccount, sum,
            credit.MainAccount.AccountPlan.AccountType ,bankAccount.AccountPlan.AccountType);

        //await _transactionService.WithdrawBankTransactionAsync(sum);
    }

    public async Task TransferMoneyAsync(int creditId, decimal sum, string mobilePhone)
    {
        var clientToSend = await UnitOfWork.ClientRepository.GetEntityAsync(x => x.MobilePhone == mobilePhone);
        if (clientToSend == null)
        {
            throw new NotFoundException("Client with mobile phone not found");
        }

        var credit = await UnitOfWork.CreditRepository.GetEntityAsync(creditId);
        if (credit.MainAccount.Balance < sum)
        {
            throw new ServiceException("Account doesn't have enough money");
        }

        var creditToSend = await UnitOfWork.CreditRepository.GetEntityAsync(x=> x.ClientId == clientToSend.ClientId);
        if (creditToSend == null)
        {
            throw new NotFoundException("Requested client doesn't have accounts");
        }
        var account = creditToSend.MainAccount;
        var sendPlan = creditToSend.MainAccount.AccountPlan.AccountType;

        await _transactionService.PerformTransactionAsync(credit.MainAccount, account, sum, credit.MainAccount.AccountPlan.AccountType, sendPlan);
    }
}
