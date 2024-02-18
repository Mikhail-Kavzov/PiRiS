using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Client;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Api.Controllers
{
    public class ClientController : ApiController
    {
        private readonly IClientManager _clientManager;

        public ClientController(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateClientAsync([FromBody] ClientDto clientDto)
        {
            await _clientManager.CreateClientAsync(clientDto);
            return Ok();
        }

        [HttpGet("Client")]
        public async Task<ActionResult<ClientDto>> GetClientAsync([FromQuery] int clientId)
        {
            var client = await _clientManager.GetClientAsync(clientId);
            return Ok(client);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateClientAsync([FromBody] ClientDto clientDto)
        {
            await _clientManager.UpdateClientAsync(clientDto);
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteClientAsync([FromQuery] int clientId)
        {
            await _clientManager.DeleteClientAsync(clientId);
            return Ok();
        }

        [HttpGet("Additionals")]
        public async Task<ActionResult<ClientAdditionalsDto>> GetAdditionalsAsync()
        {
            var clientAdditionals = await _clientManager.GetAdditionalsAsync();
            return Ok(clientAdditionals);
        }

        [HttpGet("List")]
        public async Task<ActionResult<PaginationList<ClientViewDto>>> GetClientsAsync( [FromQuery] ClientPaginationDto clientPaginationDto)
        {
            var clients = await _clientManager.GetClientsAsync(clientPaginationDto);
            return Ok(clients);
        }

    }
}
