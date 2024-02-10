using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Api.Controllers;

public class BankController : ApiController
{
    private readonly IBankManager _bankManager;

    public BankController(IBankManager bankManager)
    {
        _bankManager = bankManager;
    }

    [HttpPost("Close/Day")]
    public async Task<ActionResult> CloseBankDayAsync()
    {
        await _bankManager.CloseBankDayAsync();
        return Ok();
    }

}
