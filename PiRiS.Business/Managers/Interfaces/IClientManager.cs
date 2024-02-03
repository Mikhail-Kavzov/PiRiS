using PiRiS.Business.Dto;

namespace PiRiS.Business.Managers.Interfaces;

public interface IClientManager : IBaseManager
{
    Task DeleteClientAsync(int clientId);
    Task CreateClientAsync(ClientDto clientDto);
    Task UpdateClientAsync(ClientDto clientDto);
    Task<PaginationList<ClientViewDto>> GetClientsAsync(ClientPaginationDto clientPaginationDto);
    Task<ClientDto> GetClientAsync(int clientId);

    Task<ClientAdditionalsDto> GetAdditionalsAsync();
}
