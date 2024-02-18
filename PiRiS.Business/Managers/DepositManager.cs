using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
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
using Microsoft.Extensions.Options;

namespace PiRiS.Business.Managers;

public class DepositManager : BaseManager, IDepositManager
{
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;
    private readonly IBankService _bankService;
    private readonly CurrencyOptions _currencyOptions;

    public DepositManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger<DepositManager> logger,
        IAccountService accountService, ITransactionService transactionService, IBankService bankService, IOptions<CurrencyOptions> currencyOptions)
        : base(mapper, unitOfWork, logger)
    {
        _accountService = accountService;
        _transactionService = transactionService;
        _bankService = bankService;
        _currencyOptions = currencyOptions.Value;
    }

    public async Task CloseDepositAsync(int depositId)
    {
        var deposit = await UnitOfWork.DepositRepository.GetEntityAsync(depositId);

        if (deposit == null)
        {
            throw new NotFoundException("Deposit not found");
        }

        var currentDay = await _bankService.GetCurrentDayAsync();

        if (deposit.DepositPlan.DepositType == DepositType.Term && deposit.EndDate > currentDay)
        {
            throw new ServiceException($"Cannot close term deposit before {deposit.EndDate}");
        }

        if (deposit.Sum == 0)
        {
            throw new ServiceException("Deposit has already been closed");
        }

        var currencyName = deposit.DepositPlan.Currency.CurrencyName;
        var exchangeRate = _currencyOptions.ExchangeCourse[currencyName];
        var sumInByn = deposit.Sum * exchangeRate;

        var debitFund = await _accountService.GetFundAccountAsync();
        var fundType = debitFund.AccountPlan.AccountType;
        var depositMainType = deposit.MainAccount.AccountPlan.AccountType;
        await _transactionService.PerformTransactionAsync(debitFund, deposit.MainAccount, sumInByn, fundType, depositMainType);

        var creditBank = await _accountService.GetBankAccountAsync();
        var bankType = creditBank.AccountPlan.AccountType;

        await _transactionService.PerformTransactionAsync(deposit.MainAccount, creditBank, sumInByn, depositMainType, bankType);

        var percentSum = deposit.PercentAccount.Balance;

        var creditBankCommitted = await _accountService.GetFundAccountAsync();
        var percentType = deposit.PercentAccount.AccountPlan.AccountType;
        await _transactionService.PerformTransactionAsync(deposit.PercentAccount, creditBankCommitted, percentSum, percentType, bankType);

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

        var currencyName = await UnitOfWork.DepositRepository.GetCurrencyNameAsync(x => x.DepositNumber == newDeposit.DepositNumber);
        var exchangeRate = _currencyOptions.ExchangeCourse[currencyName];
        var sumInByn = newDeposit.Sum * exchangeRate;

        await _transactionService.PerformBankIncomeTransactionAsync(sumInByn);
        var bankAccount = await _accountService.GetBankAccountAsync();
        var fundAccount = await _accountService.GetFundAccountAsync();

        var mainAccount = await UnitOfWork.AccountRepository.GetEntityAsync(x => x.AccountNumber == newDeposit.MainAccount.AccountNumber);
        var mainType = mainAccount.AccountPlan.AccountType;

        await _transactionService.PerformTransactionAsync(bankAccount, mainAccount, sumInByn, bankAccount.AccountPlan.AccountType, mainType);
        await _transactionService.PerformTransactionAsync(mainAccount, fundAccount, sumInByn, mainType, fundAccount.AccountPlan.AccountType);

    }

    public async Task CreatePlanAsync(DepositPlanCreateDto depositPlanCreateDto)
    {
        var newPlan = Mapper.Map<DepositPlan>(depositPlanCreateDto);

        var accountPlan = await UnitOfWork.AccountPlanRepository.GetEntityAsync(x => x.Code == AccountOptions.IndividualCode);
        if (accountPlan == null)
        {
            throw new NotFoundException("Account plan for individuals not found");
        }

        newPlan.MainAccountPlanId = accountPlan.AccountPlanId;
        newPlan.PercentAccountPlanId = accountPlan.AccountPlanId;

        UnitOfWork.DepositPlanRepository.Create(newPlan);
        await UnitOfWork.DepositPlanRepository.SaveChangesAsync();
    }

    public async Task<DepositAgreementDto> GetDepositAgreementAsync()
    {
        var depositPlanTask = UnitOfWork.DepositPlanRepository.GetAllAsync();

        return new DepositAgreementDto
        {
            DepositPlans = Mapper.Map<List<DepositPlanAgreementDto>>(await depositPlanTask),
        };
    }

    public async Task<PaginationList<DepositDto>> GetDepositsAsync(DepositPaginationDto paginationDto)
    {
        Expression<Func<Deposit, bool>> predicate = null;

        if (!string.IsNullOrEmpty(paginationDto.DepositNumber))
        {
            predicate = x => x.DepositNumber.StartsWith(paginationDto.DepositNumber);
        }

        var deposits = await UnitOfWork.DepositRepository.GetListAsync(paginationDto.Skip, paginationDto.Take, predicate);
        var totalCount = await UnitOfWork.DepositRepository.CountAsync(predicate);

        var depositDtos = Mapper.Map<List<DepositDto>>(deposits);
        var currentDay = await _bankService.GetCurrentDayAsync();
        var stringRevocable = DepositType.Revocable.ToString();

        foreach (var depositDto in depositDtos)
        {
            depositDto.CanClose = (depositDto.DepositType == stringRevocable
                || depositDto.EndDate <= currentDay) && depositDto.Sum > 0;

            depositDto.CanWithdraw = depositDto.DepositType == stringRevocable && depositDto.Sum > 0
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

        await _transactionService.PerformTransactionAsync(deposit.PercentAccount, bankAccount, percentSum,
            deposit.PercentAccount.AccountPlan.AccountType, bankAccount.AccountPlan.AccountType);

        await _transactionService.WithdrawBankTransactionAsync(percentSum);
    }
}