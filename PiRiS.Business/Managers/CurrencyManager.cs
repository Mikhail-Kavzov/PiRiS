using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto.Currency;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Managers;

public class CurrencyManager : BaseManager, ICurrencyManager
{
    public CurrencyManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CurrencyManager> logger) : base(mapper, unitOfWork, logger)
    {
    }

    public async Task<List<CurrencyDto>> GetCurrenciesAsync()
    {
        var currencies = await UnitOfWork.CurrencyRepository.GetAllAsync();
        return Mapper.Map<List<CurrencyDto>>(currencies);
    }
}
