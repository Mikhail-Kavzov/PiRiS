using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Api.Controllers
{
    public class DepositController : ApiController
    {
        private readonly IDepositManager _depositManager;

        public DepositController(IDepositManager depositManager) 
        {
            _depositManager = depositManager;
        }

        [HttpGet("Agreement")]
        public async Task<ActionResult<DepositAgreementDto>> GetDepositDataAsync()
        {
            var depositAgreementDto = await _depositManager.GetDepositAgreementAsync();
            return Ok(depositAgreementDto);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateDepositAsync([FromBody] DepositCreateDto depositCreateDto)
        {
            await _depositManager.CreateDepositAsync(depositCreateDto);
            return Ok();
        }
    }
}
