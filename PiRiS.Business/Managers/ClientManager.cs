using AutoMapper;
using Microsoft.Extensions.Logging;
using PiRiS.Business.Dto;
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

    public Task CreateClientAsync(ClientDto clientDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var client = await UnitOfWork.ClientRepository.GetEntityAsync(clientId);
        if (client == null)
        {
            throw new NotFoundException($"Client with id {clientId} not found");
        }
        UnitOfWork.ClientRepository.Delete(client);

        try
        {
            await UnitOfWork.ClientRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
            throw new ServiceException($"Incorrect data while inserting");
        }
       
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

    public async Task<IEnumerable<ClientDto>> GetClientsAsync(ClientPaginationDto paginationDto)
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
        return Mapper.Map<List<ClientDto>>(clients);
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

        var updatedClient = Mapper.Map<Client>(clientDto);

        UnitOfWork.ClientRepository.Update(updatedClient);
        try
        {
            await UnitOfWork.ClientRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
            throw new ServiceException($"Incorrect data while updating");
        }
    }
}