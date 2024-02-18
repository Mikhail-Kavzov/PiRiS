using PiRiS.Business.Dto.Currency;

namespace PiRiS.Business.Managers.Interfaces;

public interface ICurrencyManager : IBaseManager
{
    Task<List<CurrencyDto>> GetCurrenciesAsync();
}
