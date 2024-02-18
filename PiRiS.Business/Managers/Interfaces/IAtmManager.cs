using PiRiS.Business.Dto.Account;
using PiRiS.Business.Dto.Credit;

namespace PiRiS.Business.Managers.Interfaces;

public interface IAtmManager : IBaseManager
{
    Task<CreditDto> LoginAsync(string creditCardNumber, string creditCardCode);
    Task WithdrawMoneyAsync(int creditId, decimal sum);
    Task<AccountDto> GetAccountAsync(string accountNumber);
    Task TransferMoneyAsync(int creditId, decimal sum, string mobilePhone);
}
