using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Currency;
using PiRiS.Business.Dto.Deposit;
using PiRiS.Business.Dto.DepositPlan;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Options;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;
using PiRiS.Data.Models.Enums;
using System.Linq.Expressions;
using PiRiS.Common.Constants;
using System.Runtime.InteropServices;

namespace PiRiS.Business.Managers;

public class DepositManager : BaseManager, IDepositManager
{
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;
    private readonly IBankService _bankService;

    public DepositManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger logger,
        IAccountService accountService, ITransactionService transactionService, IBankService bankService) 
        : base(mapper, unitOfWork, logger)
    {
        _accountService = accountService;
        _transactionService = transactionService;
        _bankService = bankService;
    }

    public async Task CloseDepositAsync(int depositId)
    {
        var deposit = await UnitOfWork.DepositRepository.GetEntityAsync(depositId);

        if (deposit == null)
        {
            throw new NotFoundException("Deposit not found");
        }

        if (deposit.DepositPlan.DepositType == DepositType.Term && deposit.EndDate > DateTime.Today)
        {
            throw new ServiceException($"Cannot close term deposit before {deposit.EndDate}");
        }

        if (deposit.Sum == 0)
        {
            throw new ServiceException("Deposit has already been closed");
        }

        var debitFund = await _accountService.GetFundAccountAsync();
        await _transactionService.PerformTransactionAsync(debitFund, deposit.MainAccount, deposit.Sum);

        var creditBank = await _accountService.GetBankAccountAsync();

        await _transactionService.PerformTransactionAsync(deposit.MainAccount, creditBank, deposit.Sum);

        var percentSum = deposit.PercentAccount.Balance;

        var creditBankCommitted = await _accountService.GetFundAccountAsync();

        await _transactionService.PerformTransactionAsync(deposit.PercentAccount, creditBankCommitted, percentSum);

        await _transactionService.WithdrawBankTransactionAsync(deposit.MainAccount.Credit + percentSum);

        deposit.Sum = 0;
        UnitOfWork.DepositRepository.Update(deposit);
        await UnitOfWork.DepositRepository.SaveChangesAsync();
    }

    public async Task CreateDepositAsync(DepositCreateDto depositCreateDto)
    {
        var isExists = await UnitOfWork.DepositRepository.ExistsAsync(x => x.DepositNumber == depositCreateDto.DepositNumber);
        if (isExists)
        {
            throw new ServiceException($"Deposit with such number already exists");
        }

        var newDeposit = Mapper.Map<Deposit>(depositCreateDto);

        var clientExists = await UnitOfWork.ClientRepository.ExistsAsync(x => x.ClientId == newDeposit.ClientId);

        if (!clientExists)
        {
            throw new NotFoundException($"Client not found");
        }

        var plan = await UnitOfWork.DepositPlanRepository.GetEntityAsync(newDeposit.DepositPlanId);
        if (plan == null)
        {
            throw new NotFoundException("Plan not found");
        }

        newDeposit.StartDate = await _bankService.GetCurrentDayAsync();

        newDeposit.EndDate = newDeposit.StartDate.AddDays(plan.DayPeriod);
        await _accountService.CreateAccountsAsync(newDeposit);
        UnitOfWork.DepositRepository.Create(newDeposit);
        await UnitOfWork.DepositRepository.SaveChangesAsync();

        await _transactionService.PerformBankDebitTransactionAsync(newDeposit.Sum);
        var bankAccount = await _accountService.GetBankAccountAsync();
        var fundAccount = await _accountService.GetFundAccountAsync();

        await _transactionService.PerformTransactionAsync(bankAccount, newDeposit.MainAccount, newDeposit.Sum);
        await _transactionService.PerformTransactionAsync(newDeposit.MainAccount, fundAccount, newDeposit.Sum);

    }

    public async Task CreatePlanAsync(DepositPlanCreateDto depositPlanCreateDto)
    {
        var newPlan = Mapper.Map<DepositPlan>(depositPlanCreateDto);

        var accountPlan = await UnitOfWork.AccountPlanRepository.GetEntityAsync(x => x.Code == AccountOptions.IndividualCode);
        if (accountPlan == null)
        {
            throw new NotFoundException("Account plan for individuals not found");
        }

        newPlan.MainAccountPlan = accountPlan;
        newPlan.PercentAccountPlan = accountPlan;

        UnitOfWork.DepositPlanRepository.Create(newPlan);
        await UnitOfWork.DepositPlanRepository.SaveChangesAsync();
    }

    public async Task<DepositAgreementDto> GetDepositAgreementAsync()
    {
        var depositPlanTask = UnitOfWork.DepositPlanRepository.GetAllAsync();
        var currencyTask = UnitOfWork.CurrencyRepository.GetAllAsync();

        return new DepositAgreementDto
        {
            DepositPlans = Mapper.Map<List<DepositPlanAggreementDto>>(await depositPlanTask),
            Currencies = Mapper.Map<List<CurrencyDto>>(await currencyTask),
        };
    }

    public async Task<PaginationList<DepositDto>> GetDepositsAsync(DepositPaginationDto paginationDto)
    {
        Expression<Func<Deposit, bool>> predicate = null;

        if (!string.IsNullOrEmpty(paginationDto.DepositNumber))
        {
            predicate = x => x.DepositNumber == paginationDto.DepositNumber;
        }

        var deposits = await UnitOfWork.DepositRepository.GetListAsync(paginationDto.Skip, paginationDto.Take, predicate);
        var totalCount = await UnitOfWork.DepositRepository.CountAsync(predicate);

        var depositDtos = Mapper.Map<List<DepositDto>>(deposits);
        var currentDay = await _bankService.GetCurrentDayAsync();

        foreach(var depositDto in depositDtos)
        {
            depositDto.CanClose = (depositDto.DepositType == DepositType.Revocable 
                || depositDto.EndDate < currentDay) && depositDto.Sum > 0;

            depositDto.CanWithdraw = depositDto.DepositType == DepositType.Revocable && depositDto.Sum > 0
            && (currentDay - depositDto.StartDate).TotalDays % BankParams.DaysInMonth == 0;
        }

        return new PaginationList<DepositDto>
        {
            Items = depositDtos,
            TotalCount = totalCount
        };
    }

    public async Task<PaginationList<DepositPlanDto>> GetPlansAsync(PaginationDto paginationDto)
    {
        var depositPlans = await UnitOfWork.DepositPlanRepository.GetListAsync(paginationDto.Skip, paginationDto.Take);
        var totalCount = await UnitOfWork.DepositPlanRepository.CountAsync();

        return new PaginationList<DepositPlanDto>
        {
            Items = Mapper.Map<List<DepositPlanDto>>(depositPlans),
            TotalCount = totalCount
        };
    }

    public async Task WithdrawPercentsAsync(int depositId)
    {
        var deposit = await UnitOfWork.DepositRepository.GetEntityAsync(depositId);

        if (deposit == null)
        {
            throw new NotFoundException("Deposit not found");
        }

        if (deposit.DepositPlan.DepositType == DepositType.Term)
        {
            throw new ServiceException("Cannot withdraw percent for term deposit before end of date");
        }

        var currentDay = await _bankService.GetCurrentDayAsync();

        if ((currentDay - deposit.StartDate).TotalDays % BankParams.DaysInMonth != 0)
        {
            throw new ServiceException("You can witdraw percents only one time in month");
        }

        var percentSum = deposit.PercentAccount.Balance;
        var bankAccount = await _accountService.GetBankAccountAsync();

        await _transactionService.PerformTransactionAsync(deposit.PercentAccount, bankAccount, percentSum);

        await _transactionService.WithdrawBankTransactionAsync(percentSum);
    }
}