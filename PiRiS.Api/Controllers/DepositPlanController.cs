using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Dto;
using PiRiS.Business.Dto.DepositPlan;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Api.Controllers
{
    public class DepositPlanController : ApiController
    {
        private readonly IDepositManager _depositManager;

        public DepositPlanController(IDepositManager depositManager)
        {
            _depositManager = depositManager;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreatePlanAsync([FromBody] DepositPlanCreateDto depositPlanCreateDto)
        {
            await _depositManager.CreatePlanAsync(depositPlanCreateDto);
            return Ok();
        }

        [HttpGet("List")]
        public async Task<ActionResult<PaginationList<DepositPlanDto>>> GetListAsync([FromQuery] PaginationDto paginationDto)
        {
            var plans = await _depositManager.GetPlansAsync(paginationDto);
            return Ok(plans);
        }

    }
}
