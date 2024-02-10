using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Client;
using PiRiS.Business.Enums;
using PiRiS.Business.Exceptions;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Data.Models;
using PiRiS.Data.UnitOfWork;
using System.Linq.Expressions;

namespace PiRiS.Business.Managers;

public class ClientManager : BaseManager, IClientManager
{
    public ClientManager(IMapper mapper, IUnitOfWork unitOfWork, ILogger<ClientManager> logger)
        : base(mapper, unitOfWork, logger)
    {
    }

    public async Task CreateClientAsync(ClientDto clientDto)
    {
        await CheckClientConstrains(clientDto);

        var client = Mapper.Map<Client>(clientDto);
        UnitOfWork.ClientRepository.Create(client);

        await UnitOfWork.ClientRepository.SaveChangesAsync();
    }

    private async Task CheckClientConstrains(ClientDto clientDto)
    {
        var hasIdNumber = await UnitOfWork.ClientRepository
           .ExistsAsync(x => x.IdentificationNumber == clientDto.IdentificationNumber);

        if (hasIdNumber)
        {
            throw new ServiceException($"Client with identification number {clientDto.IdentificationNumber} already exists");
        }

        var hasPassport = await UnitOfWork.ClientRepository
            .ExistsAsync(x => x.PassportSeries == clientDto.PassportSeries && x.PassportNumber == clientDto.PassportNumber);

        if (hasPassport)
        {
            throw new ServiceException($"Client with passport {clientDto.PassportSeries} {clientDto.PassportNumber} already exists");
        }

        var hasClientNames = await UnitOfWork.ClientRepository
            .ExistsAsync(x=> x.Surname == clientDto.Surname && x.FirstName == clientDto.FirstName && x.LastName == x.LastName);

        if (hasClientNames)
        {
            throw new ServiceException($"Client with such Surname, Firstname, Lastname already exists");
        }
    }

    private async Task CheckClientConstrainsOnUpdate(ClientDto clientDto)
    {
        var hasIdNumber = await UnitOfWork.ClientRepository
           .ExistsAsync(x => x.IdentificationNumber == clientDto.IdentificationNumber && clientDto.ClientId !=x.ClientId);

        if (hasIdNumber)
        {
            throw new ServiceException($"Client with identification number {clientDto.IdentificationNumber} already exists");
        }

        var hasPassport = await UnitOfWork.ClientRepository
            .ExistsAsync(x => x.PassportSeries == clientDto.PassportSeries && x.PassportNumber == clientDto.PassportNumber && clientDto.ClientId != x.ClientId);

        if (hasPassport)
        {
            throw new ServiceException($"Client with passport {clientDto.PassportSeries} {clientDto.PassportNumber} already exists");
        }

        var hasClientNames = await UnitOfWork.ClientRepository
            .ExistsAsync(x => x.Surname == clientDto.Surname && x.FirstName == clientDto.FirstName && x.LastName == x.LastName && clientDto.ClientId != x.ClientId);

        if (hasClientNames)
        {
            throw new ServiceException($"Client with such Surname, Firstname, Lastname already exists");
        }
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var client = await UnitOfWork.ClientRepository.GetEntityAsync(clientId);
        if (client == null)
        {
            throw new NotFoundException($"Client with id {clientId} not found");
        }

        var hasCredits = await UnitOfWork.CreditRepository.ExistsAsync(x => x.ClientId == clientId && x.Sum != 0);
        if (hasCredits)
        {
            throw new ServiceException("Client has unclosed credits");
        }

        UnitOfWork.ClientRepository.Delete(client);

        await UnitOfWork.ClientRepository.SaveChangesAsync();

    }

    public async Task<ClientDto> GetClientAsync(int clientId)
    {
        var client = await UnitOfWork.ClientRepository.GetEntityAsync(clientId);
        if (client == null)
        {
            throw new NotFoundException($"Client with id {clientId} not found");
        }
        return Mapper.Map<ClientDto>(client);
    }

    public async Task<PaginationList<ClientViewDto>> GetClientsAsync(ClientPaginationDto paginationDto)
    {
        Expression<Func<Client, bool>> predicate = null;

        if (!string.IsNullOrEmpty(paginationDto.Surname))
        {
            predicate = x => x.Surname.Contains(paginationDto.Surname);
        }

        var isAscending = paginationDto.SortDirection == SortDirection.Ascending;
        Expression<Func<Client, object>> sort = null;

        switch (paginationDto.SortField)
        {
            case ClientSortField.Surname:
                {
                    sort = x => x.Surname;
                    break;
                }
            default:
                {
                    throw new NotImplementedException($"Client sort field {paginationDto.SortField} not supported");
                }

        }

        var clients = await UnitOfWork.ClientRepository
            .GetListAsync(paginationDto.Skip, paginationDto.Take, predicate, sort, isAscending);
        var totalCount = await UnitOfWork.ClientRepository.CountAsync(predicate);

       var clientsDto = Mapper.Map<List<ClientViewDto>>(clients);
        return new PaginationList<ClientViewDto>
        {
            Items = clientsDto,
            TotalCount = totalCount
        };
    }

    public async Task UpdateClientAsync(ClientDto clientDto)
    {
        if (!clientDto.ClientId.HasValue)
        {
            throw new ServiceException($"Specify client id");
        }

        var clientId = clientDto.ClientId.Value;
        var client = await UnitOfWork.ClientRepository.GetEntityAsync(clientId, trackChanges: false);

        if (client == null)
        {
            throw new NotFoundException($"Client with id {clientId} not found");
        }

        await CheckClientConstrainsOnUpdate(clientDto);

        var updatedClient = Mapper.Map<Client>(clientDto);

        UnitOfWork.ClientRepository.Update(updatedClient);

        await UnitOfWork.ClientRepository.SaveChangesAsync();
    }

    public async Task<ClientAdditionalsDto> GetAdditionalsAsync()
    {
        var disabilityTask = UnitOfWork.DisabilityRepository.GetAllAsync();
        var cityTask = UnitOfWork.CityRepository.GetAllAsync();
        var citizenshipTask = UnitOfWork.CitizenshipRepository.GetAllAsync();
        var familyStatusTask = UnitOfWork.FamilyStatusRepository.GetAllAsync();

        var disabilities = await disabilityTask;
        var cities = await cityTask;
        var citizenships = await citizenshipTask;
        var familyStatuses = await familyStatusTask;

        return new ClientAdditionalsDto
        {
            Disabilities = Mapper.Map<List<DisabilityDto>>(disabilities),
            Cities = Mapper.Map<List<CityDto>>(cities),
            FamilyStatuses = Mapper.Map<List<FamilyStatusDto>>(familyStatuses),
            Citizenships = Mapper.Map<List<CitizenshipDto>>(citizenships),
        };
    }
}