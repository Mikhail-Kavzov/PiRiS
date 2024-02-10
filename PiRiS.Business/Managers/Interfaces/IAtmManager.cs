using PiRiS.Business.Dto.Atm;
using PiRiS.Business.Dto.Credit;

namespace PiRiS.Business.Managers.Interfaces;

public interface IAtmManager : IBaseManager
{
    Task<CreditDto> LoginAsync(string creditCardNumber, string creditCardCode);
    Task WithdrawMoneyAsync(int creditId, decimal sum);
}
