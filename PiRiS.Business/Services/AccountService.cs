using AutoMapper;
using PiRiS.Business.Options;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Common.Constants;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Services;

public class AccountService : BaseService, IAccountService
{
    public AccountService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task CreateAccountsAsync(Deposit deposit)
    {
        var random = new Random();
        var depositPlan = deposit.DepositPlan ?? await UnitOfWork.DepositPlanRepository.GetEntityAsync(deposit.DepositPlanId);
        var mainAccount = CreateAccount(depositPlan.MainAccountPlan, deposit.ClientId, random.Next());
        var percentAccount = CreateAccount(depositPlan.PercentAccountPlan, deposit.ClientId, random.Next());
        deposit.MainAccount = mainAccount;
        deposit.PercentAccount = percentAccount;
    }

    private Account CreateAccount(AccountPlan accountPlan, int clientId, int order)
    {
        return new Account
        {
            Debit = 0,
            Credit = 0,
            Balance = 0,
            AccountPlan = accountPlan,
            AccountNumber = GenerateAccountNumber(accountPlan.Code, clientId, order),
        };
    }

    private static string GenerateAccountNumber(string accountCode, int clientId, int order)
    {
        if (clientId == 0)
            return accountCode + "000000000";
        return $"{accountCode}{clientId:0000}{order:0000}1";
    }

    public async Task CreateAccountsAsync(Credit credit)
    {
        var random = new Random();
        var depositPlan = credit.CreditPlan ?? await UnitOfWork.CreditPlanRepository.GetEntityAsync(credit.CreditPlanId);
        var mainAccount = CreateAccount(depositPlan.MainAccountPlan, credit.ClientId, random.Next());
        var percentAccount = CreateAccount(depositPlan.PercentAccountPlan, credit.ClientId, random.Next());
        credit.MainAccount = mainAccount;
        credit.PercentAccount = percentAccount;
    }

    public async Task<Account> GetBankAccountAsync()
    {
        return await UnitOfWork.AccountRepository.GetEntityAsync(x=> x.AccountPlan.Code == AccountOptions.BankCode);
    }

    public async Task<Account> GetFundAccountAsync()
    {
        return await UnitOfWork.AccountRepository.GetEntityAsync(x => x.AccountPlan.Code == AccountOptions.FundCode);
    }
}
