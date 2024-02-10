using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto.Currency;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Api.Controllers;

public class CurrencyController : ApiController
{
    private readonly ICurrencyManager _currencyManager;

    public CurrencyController(ICurrencyManager currencyManager)
    {
        _currencyManager = currencyManager;
    }

    [HttpGet("List")]
    public async Task<ActionResult<List<CurrencyDto>>> GetCurrenciesAsync()
    {
        var currencies = await _currencyManager.GetCurrenciesAsync();
        return Ok(currencies);
    }
}
