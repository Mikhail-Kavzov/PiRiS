using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Transaction;
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

    [HttpGet("Transactions/List")]
    public async Task<ActionResult<PaginationList<TransactionDto>>> GetTransactionsAsync([FromQuery] PaginationDto paginationDto)
    {
        var transactions = await _bankManager.GetTransactionsAsync(paginationDto);
        return Ok(transactions);
    }

}
