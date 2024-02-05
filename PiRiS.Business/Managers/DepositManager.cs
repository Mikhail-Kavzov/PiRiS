using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;

namespace PiRiS.Business.Managers;

public class DepositManager : BaseManager, IDepositManager
{
    public DepositManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger logger) : base(mapper, unitOfWork, logger)
    {
    }

    public async Task CreateDepositAsync(DepositCreateDto depositCreateDto)
    {
        if (depositCreateDto.StartDate > DateTime.Today)
        {
            throw new ServiceException($"Start date cannot be more than current day");
        }

        var newDeposit = Mapper.Map<Deposit>(depositCreateDto);

        var clientExists = await UnitOfWork.ClientRepository.ExistsAsync(x=> x.ClientId == newDeposit.ClientId);

        if (!clientExists)
        {
            throw new NotFoundException($"Client not found");
        }

        var currencyExists = await UnitOfWork.CurrencyRepository.ExistsAsync(x=> x.CurrencyId == newDeposit.CurrencyId);    
        if (!currencyExists)
        {
            throw new NotFoundException($"Currency not found");
        }

        var plan = await UnitOfWork.DepositPlanRepository.GetEntityAsync(newDeposit.DepositPlanId);
        if(plan == null)
        {
            throw new NotFoundException("Plan not found");
        }

        newDeposit.EndDate = newDeposit.StartDate.AddDays(plan.DayPeriod);
        //create accounts here
        UnitOfWork.DepositRepository.Create(newDeposit);
        await UnitOfWork.DepositRepository.SaveChangesAsync();

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
}