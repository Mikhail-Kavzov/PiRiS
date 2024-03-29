﻿using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto.Account;
using PiRiS.Business.Dto.Atm;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Services.Interfaces;

namespace PiRiS.Api.Controllers;

public class AtmController : ApiController
{
    private readonly IAtmManager _atmManager;
    private readonly IBankService _bankService;

    public AtmController(IAtmManager atmManager, IBankService bankService)
    {
        _atmManager = atmManager;
        _bankService = bankService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult> LoginAsync([FromBody] AtmLoginDto loginDto)
    {
        await _atmManager.LoginAsync(loginDto.CreditCardNumber, loginDto.CreditCardCode);
        return Ok();
    }

    [HttpPost("Withdraw")]
    public async Task<ActionResult<AtmReportDto>> WithdrawMoneyAsync([FromBody] WithdrawMoneyDto withdrawDto)
    {
        var creditDto = await _atmManager.LoginAsync(withdrawDto.CreditCardNumber, withdrawDto.CreditCardCode);
        await _atmManager.WithdrawMoneyAsync(creditDto.CreditId, withdrawDto.Sum);

        var report = new AtmReportDto
        {
            CreditCardNumber = withdrawDto.CreditCardNumber,
            OperationDate = await _bankService.GetCurrentDayAsync(),
            OperationName = "Withdraw money",
            Sum = withdrawDto.Sum,
            CurrencyName = "BYN",
        };

        return Ok(report);
    }

    [HttpPost("Account")]
    public async Task<ActionResult<AccountDto>> GetBalanceAsync([FromBody] AtmLoginDto loginDto)
    {
        var creditDto = await _atmManager.LoginAsync(loginDto.CreditCardNumber, loginDto.CreditCardCode);
        var accountDto = await _atmManager.GetAccountAsync(creditDto.MainAccountNumber);
        return Ok(accountDto);
    }

    [HttpPost("Transfer")]
    public async Task<ActionResult<AtmReportDto>> TransferMoneyAsync([FromBody] TransferMoneyDto transferMoneyDto)
    {
        var creditDto = await _atmManager.LoginAsync(transferMoneyDto.CreditCardNumber, transferMoneyDto.CreditCardCode);
        await _atmManager.TransferMoneyAsync(creditDto.CreditId, transferMoneyDto.Sum, transferMoneyDto.MobilePhone);

        var report = new AtmReportDto
        {
            CreditCardNumber = transferMoneyDto.CreditCardNumber,
            OperationDate = await _bankService.GetCurrentDayAsync(),
            OperationName = $"Transfer money to {transferMoneyDto.MobilePhone}",
            Sum = transferMoneyDto.Sum,
            CurrencyName = "BYN",
        };
        return Ok(report);
    }
}