using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;

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

        var newcredit = Mapper.Map<Credit>(creditCreateDto);

        var clientExists = await UnitOfWork.ClientRepository.ExistsAsync(x => x.ClientId == newcredit.ClientId);

        if (!clientExists)
        {
            throw new NotFoundException($"Client not found");
        }

        var currencyExists = await UnitOfWork.CurrencyRepository.ExistsAsync(x => x.CurrencyId == newcredit.CurrencyId);
        if (!currencyExists)
        {
            throw new NotFoundException($"Currency not found");
        }

        var plan = await UnitOfWork.CreditPlanRepository.GetEntityAsync(newcredit.CreditPlanId);
        if (plan == null)
        {
            throw new NotFoundException("Plan not found");
        }
    }

    public Task<CreditAgreementDto> GetCreditAgreementAsync()
    {
        throw new NotImplementedException();
    }
}
