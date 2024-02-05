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
using System.Linq.Expressions;

namespace PiRiS.Business.Managers;

public class DepositManager : BaseManager, IDepositManager
{
    private readonly IAccountService _accountService;

    public DepositManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger logger, IAccountService accountService) 
        : base(mapper, unitOfWork, logger)
    {
        _accountService = accountService;
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

        newDeposit.EndDate = newDeposit.StartDate.AddDays(plan.DayPeriod);
        await _accountService.CreateAccountsAsync(newDeposit);
        UnitOfWork.DepositRepository.Create(newDeposit);
        await UnitOfWork.DepositRepository.SaveChangesAsync();

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

        return new PaginationList<DepositDto>
        {
            Items = Mapper.Map<List<DepositDto>>(deposits),
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
}