using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Credit;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Api.Controllers
{

    public class CreditController : ApiController
    {
        private readonly ICreditManager _creditManager;

        public CreditController(ICreditManager creditManager) 
        { 
            _creditManager = creditManager;
        }

        [HttpGet("Agreement")]
        public async Task<ActionResult<CreditAgreementDto>> GetCreditAgreementAsync()
        {
            var agreement = await _creditManager.GetCreditAgreementAsync();
            return Ok(agreement);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateCreditAsync([FromBody] CreditCreateDto creditCreateDto)
        {
            await _creditManager.CreateCreditAsync(creditCreateDto);
            return Ok();
        }

        [HttpGet("List")]
        public async Task<ActionResult<PaginationList<CreditDto>>> GetCreditsAsync([FromQuery] CreditPaginationDto creditPaginationDto)
        {
            var credits = await _creditManager.GetCreditsAsync(creditPaginationDto);
            return Ok(credits);
        }

    }
}
