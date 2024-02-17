using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Credit;
using PiRiS.Business.Dto.CreditPlan;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Options;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Common.Constants;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;
using System.Linq.Expressions;
using PiRiS.Data.Models.Enums;
using Microsoft.Extensions.Options;

namespace PiRiS.Business.Managers;

public class CreditManager : BaseManager, ICreditManager
{
    private readonly IAccountService _accountService;
    private readonly IBankService _bankService;
    private readonly ITransactionService _transactionService;
    private readonly CurrencyOptions _currencyOptions;

    public CreditManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CreditManager> logger, IAccountService accountService,
        ITransactionService transactionService, IBankService bankService, IOptions<CurrencyOptions> currencyOptions)
        : base(mapper, unitOfWork, logger)
    {
        _accountService = accountService;
        _bankService = bankService;
        _transactionService = transactionService;
        _currencyOptions = currencyOptions.Value;
    }

    public async Task CreateCreditAsync(CreditCreateDto creditCreateDto)
    {
        var isExists = await UnitOfWork.CreditRepository.ExistsAsync(x => x.CreditNumber == creditCreateDto.CreditNumber
        || x.CreditCardNumber == creditCreateDto.CreditCardNumber);
        if (isExists)
        {
            throw new ServiceException($"Credit with such number or credit card already exists");
        }

        var newCredit = Mapper.Map<Credit>(creditCreateDto);

        var clientExists = await UnitOfWork.ClientRepository.ExistsAsync(x => x.ClientId == newCredit.ClientId);

        if (!clientExists)
        {
            throw new NotFoundException($"Client not found");
        }

        var plan = await UnitOfWork.CreditPlanRepository.GetEntityAsync(newCredit.CreditPlanId);
        if (plan == null)
        {
            throw new NotFoundException("Plan not found");
        }

        var currentDay = await _bankService.GetCurrentDayAsync();

        newCredit.StartDate = currentDay;
        newCredit.EndDate = newCredit.StartDate.AddMonths(plan.MonthPeriod);

        await _accountService.CreateAccountsAsync(newCredit);
        UnitOfWork.CreditRepository.Create(newCredit);
        await UnitOfWork.CreditRepository.SaveChangesAsync();

        var mainAccount = await UnitOfWork.AccountRepository.GetEntityAsync(x => x.AccountNumber == newCredit.MainAccount.AccountNumber);
        var currencyName = await UnitOfWork.CreditRepository.GetCurrencyNameAsync(x=> x.CreditNumber == newCredit.CreditNumber);

        var exchangeRate = _currencyOptions.ExchangeCourse[currencyName];

        var sumInByn = newCredit.Sum * exchangeRate;

        await _transactionService
            .PerformTransactionAsync(await _accountService.GetFundAccountAsync(), mainAccount, sumInByn);

        await _transactionService.PerformTransactionAsync(mainAccount, await _accountService.GetBankAccountAsync(), sumInByn);
        await _transactionService.WithdrawBankTransactionAsync(sumInByn);

    }

    public async Task CreatePlanAsync(CreditPlanCreateDto planCreateDto)
    {
        var newPlan = Mapper.Map<CreditPlan>(planCreateDto);

        var accountPlan = await UnitOfWork.AccountPlanRepository.GetEntityAsync(x => x.Code == AccountOptions.CreditCode);

        if (accountPlan == null)
        {
            throw new NotFoundException("Account plan for credits not found");
        }

        newPlan.MainAccountPlanId = accountPlan.AccountPlanId;
        newPlan.PercentAccountPlanId = accountPlan.AccountPlanId;

        UnitOfWork.CreditPlanRepository.Create(newPlan);
        await UnitOfWork.CreditPlanRepository.SaveChangesAsync();
    }

    public async Task<CreditAgreementDto> GetCreditAgreementAsync()
    {
        var creditTask = UnitOfWork.CreditPlanRepository.GetAllAsync();

        return new CreditAgreementDto
        {
            CreditPlans = Mapper.Map<List<CreditPlanAgreementDto>>(await creditTask),
        };
    }

    public async Task<PaginationList<CreditDto>> GetCreditsAsync(CreditPaginationDto paginationDto)
    {
        Expression<Func<Credit, bool>> predicate = null;

        if (!string.IsNullOrEmpty(paginationDto.CreditNumber))
        {
            predicate = x => x.CreditNumber.StartsWith(paginationDto.CreditNumber);
        }

        var credits = await UnitOfWork.CreditRepository.GetListAsync(paginationDto.Skip, paginationDto.Take, predicate);
        var totalCount = await UnitOfWork.CreditRepository.CountAsync(predicate);
        var creditDtos = Mapper.Map<List<CreditDto>>(credits);
        var currentDay = await _bankService.GetCurrentDayAsync();

        foreach (var credit in creditDtos)
        {
            credit.CanClose = credit.EndDate <= currentDay && credit.Sum > 0;
            credit.CanPayPercents = (currentDay - credit.StartDate).TotalDays % BankParams.DaysInMonth == 0 && credit.Sum > 0;
        }

        return new PaginationList<CreditDto>
        {
            Items = creditDtos,
            TotalCount = totalCount
        };
    }

    public async Task<PaginationList<CreditPlanDto>> GetPlansAsync(PaginationDto paginationDto)
    {
        var creditPlans = await UnitOfWork.CreditPlanRepository.GetListAsync(paginationDto.Skip, paginationDto.Take);
        var totalCount = await UnitOfWork.CreditPlanRepository.CountAsync();

        return new PaginationList<CreditPlanDto>
        {
            Items = Mapper.Map<List<CreditPlanDto>>(creditPlans),
            TotalCount = totalCount
        };
    }

    public async Task<CreditScheduleDto> GetScheduleAsync(int creditId)
    {
        var credit = await UnitOfWork.CreditRepository.GetEntityAsync(creditId);

        if (credit == null)
        {
            throw new NotFoundException("Credit not found");
        }

        var currencyName = credit.CreditPlan.Currency.CurrencyName;
        var currentDay = await _bankService.GetCurrentDayAsync();
        var shcheduleDto = new CreditScheduleDto
        {
            CreditId = creditId,
            CurrentDay = currentDay,
            Schedule = new Dictionary<DateTime, double>(),
            CurrencyName = currencyName,
        };

        var monthes = credit.CreditPlan.MonthPeriod;

        var monthPercent = credit.CreditPlan.Percent / BankParams.MonthInYear;

        if (credit.CreditPlan.CreditType == CreditType.Annuity)
        {
            var temp = Math.Pow(1 + monthPercent, monthes);
            var monthPayment = (monthPercent * temp / (temp - 1)/ BankParams.PercentDelimiter) * (double)credit.Sum;

            var paymentDate = credit.StartDate.AddMonths(1);

            for (int i=0; i< monthes; i++)
            {
                shcheduleDto.Schedule.Add(paymentDate, monthPayment);
                paymentDate = paymentDate.AddMonths(1);
            }
        }
        else
        {
            var creditRest = (double)credit.Sum;
            var monthCreditDebt = (double)credit.Sum / monthes;
            var creditMonthPercent = credit.CreditPlan.Percent / BankParams.PercentDelimiter / BankParams.DaysInYear * BankParams.DaysInMonth;

            DateTime paymentDate = credit.StartDate.AddMonths(1);
            for (int i = 0; i < monthes; i++)
            {
                double currMonthPayment = monthCreditDebt + creditRest * creditMonthPercent;
                shcheduleDto.Schedule.Add(paymentDate, currMonthPayment);

                creditRest -= monthCreditDebt;
                paymentDate = paymentDate.AddMonths(1);
            }

        }
        return shcheduleDto;
    }

    public async Task PayPercentsAsync(int creditId)
    {
        var credit = await UnitOfWork.CreditRepository.GetEntityAsync(creditId);

        if (credit == null)
        {
            throw new NotFoundException("Credit not found");
        }

        if (credit.Sum == 0)
        {
            throw new ServiceException("Credit has already been closed");
        }

        var sum = Math.Abs(credit.PercentAccount.Balance);

        await _transactionService.WithdrawBankTransactionAsync(sum);
        await _transactionService.PerformTransactionAsync(await _accountService.GetBankAccountAsync(), credit.PercentAccount, sum);
    }

    public async Task CloseCreditAsync(int creditId)
    {

        var credit = await UnitOfWork.CreditRepository.GetEntityAsync(creditId);

        if (credit == null)
        {
            throw new NotFoundException("Credit not found");
        }

        var currentDay = await _bankService.GetCurrentDayAsync();

        if (credit.EndDate > currentDay)
        {
            throw new ServiceException($"Cannot close credit before {credit.EndDate}");
        }

        if (credit.Sum == 0)
        {
            throw new ServiceException("Credit has already been closed");
        }
        var currencyName = credit.CreditPlan.Currency.CurrencyName;
        var exchangeRate = _currencyOptions.ExchangeCourse[currencyName];
        var sumInByn = credit.Sum * exchangeRate;

        await _transactionService.PerformBankDebitTransactionAsync(sumInByn);

        await _transactionService.PerformTransactionAsync(await _accountService.GetBankAccountAsync(), credit.MainAccount, sumInByn);
        await _transactionService.PerformTransactionAsync(credit.MainAccount, await _accountService.GetFundAccountAsync(), sumInByn);

        await _transactionService.PerformBankDebitTransactionAsync(credit.PercentAccount.Balance);
        await _transactionService.PerformTransactionAsync(await _accountService.GetBankAccountAsync(), credit.PercentAccount, credit.PercentAccount.Balance);

        credit.Sum = 0;
        UnitOfWork.CreditRepository.Update(credit);
        await UnitOfWork.CreditRepository.SaveChangesAsync();
    }
}
