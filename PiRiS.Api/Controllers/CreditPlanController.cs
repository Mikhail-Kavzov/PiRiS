using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.CreditPlan;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Api.Controllers
{
    public class CreditPlanController : ApiController
    {
        private readonly ICreditManager _creditManager;

        public CreditPlanController(ICreditManager creditManager) 
        { 
            _creditManager = creditManager;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreatePlanAsync([FromBody] CreditPlanCreateDto creditPlanCreateDto)
        {
            await _creditManager.CreatePlanAsync(creditPlanCreateDto);
            return Ok();
        }

        [HttpGet("List")]
        public async Task<ActionResult<PaginationList<CreditPlanDto>>> GetPlansAsync([FromQuery] PaginationDto paginationDto)
        {
            var plans = await _creditManager.GetPlansAsync(paginationDto);
            return Ok(plans);
        }
    }
}
