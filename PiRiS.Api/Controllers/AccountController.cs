using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Account;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Api.Controllers;

public class AccountController : ApiController
{
    private readonly IAccountManager _accountManager;

    public AccountController(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }


    [HttpGet("List")]
    public async Task<ActionResult<PaginationList<AccountDto>>> GetAccountsAsync([FromQuery] AccountPaginationDto accountPaginationDto)
    {
        var accounts = await _accountManager.GetAccountsAsync(accountPaginationDto);
        return Ok(accounts);
    }
}
