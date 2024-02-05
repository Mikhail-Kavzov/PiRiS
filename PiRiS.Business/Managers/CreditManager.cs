using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Credit;
using PiRiS.Business.Dto.CreditPlan;
using PiRiS.Business.Dto.Currency;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Options;
using PiRiS.Business.Services.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;
using System.Linq.Expressions;

namespace PiRiS.Business.Managers;

public class CreditManager : BaseManager, ICreditManager
{
    private readonly IAccountService _accountService;

    public CreditManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger logger, IAccountService accountService)
        : base(mapper, unitOfWork, logger)
    {
        _accountService = accountService;
    }

    public async Task CreateCreditAsync(CreditCreateDto creditCreateDto)
    {
        var isExists = await UnitOfWork.CreditRepository.ExistsAsync(x => x.CreditNumber == creditCreateDto.CreditNumber);
        if (isExists)
        {
            throw new ServiceException($"Credit with such number already exists");
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

        newCredit.StartDate = default;
        newCredit.EndDate = newCredit.StartDate.AddMonths(plan.MonthPeriod);

        await _accountService.CreateAccountsAsync(newCredit);
        UnitOfWork.CreditRepository.Create(newCredit);
        await UnitOfWork.CreditRepository.SaveChangesAsync();

    }

    public async Task CreatePlanAsync(CreditPlanCreateDto planCreateDto)
    {
        var newPlan = Mapper.Map<CreditPlan>(planCreateDto);

        var accountPlan = await UnitOfWork.AccountPlanRepository.GetEntityAsync(x => x.Code == AccountOptions.CreditCode);

        if (accountPlan == null)
        {
            throw new NotFoundException("Account plan for credits not found");
        }

        newPlan.MainAccountPlan = accountPlan;
        newPlan.PercentAccountPlan = accountPlan;

        UnitOfWork.CreditPlanRepository.Create(newPlan);
        await UnitOfWork.CreditPlanRepository.SaveChangesAsync();
    }

    public async Task<CreditAgreementDto> GetCreditAgreementAsync()
    {
        var creditTask = UnitOfWork.CreditPlanRepository.GetAllAsync();
        var currencyTask = UnitOfWork.CurrencyRepository.GetAllAsync();

        return new CreditAgreementDto
        {
            CreditPlans = Mapper.Map<List<CreditPlanAgreementDto>>(await creditTask),
            Currencies = Mapper.Map<List<CurrencyDto>>(await currencyTask),
        };
    }

    public async Task<PaginationList<CreditDto>> GetCreditsAsync(CreditPaginationDto paginationDto)
    {
        Expression<Func<Credit, bool>> predicate = null;

        if (!string.IsNullOrEmpty(paginationDto.CreditNumber))
        {
            predicate = x => x.CreditNumber == paginationDto.CreditNumber;
        }

        var credits = await UnitOfWork.CreditRepository.GetListAsync(paginationDto.Skip, paginationDto.Take, predicate);
        var totalCount = await UnitOfWork.CreditRepository.CountAsync(predicate);

        return new PaginationList<CreditDto>
        {
            Items = Mapper.Map<List<CreditDto>>(credits),
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
}
