using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;
using System.Linq.Expressions;

namespace PiRiS.Business.Managers;

public class CreditManager : BaseManager, ICreditManager
{
    public CreditManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger logger) : base(mapper, unitOfWork, logger)
    {
    }

    public async Task CreateCreditAsync(CreditCreateDto creditCreateDto)
    {
        if (creditCreateDto.StartDate > DateTime.Today)
        {
            throw new ServiceException($"Start date cannot be more than current day");
        }

        var newCredit = Mapper.Map<Credit>(creditCreateDto);

        var clientExists = await UnitOfWork.ClientRepository.ExistsAsync(x => x.ClientId == newCredit.ClientId);

        if (!clientExists)
        {
            throw new NotFoundException($"Client not found");
        }

        var currencyExists = await UnitOfWork.CurrencyRepository.ExistsAsync(x => x.CurrencyId == newCredit.CurrencyId);
        if (!currencyExists)
        {
            throw new NotFoundException($"Currency not found");
        }

        var plan = await UnitOfWork.CreditPlanRepository.GetEntityAsync(newCredit.CreditPlanId);
        if (plan == null)
        {
            throw new NotFoundException("Plan not found");
        }

        newCredit.EndDate = newCredit.StartDate.AddMonths(plan.MonthPeriod);
        //create accounts here
        UnitOfWork.CreditRepository.Create(newCredit);
        await UnitOfWork.CreditRepository.SaveChangesAsync();

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
}
