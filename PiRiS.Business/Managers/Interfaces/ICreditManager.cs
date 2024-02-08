using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Credit;
using PiRiS.Business.Dto.CreditPlan;

namespace PiRiS.Business.Managers.Interfaces;

public interface ICreditManager : IBaseManager
{
    Task<CreditAgreementDto> GetCreditAgreementAsync();
    Task CreateCreditAsync(CreditCreateDto creditCreateDto);
    Task<PaginationList<CreditDto>> GetCreditsAsync(CreditPaginationDto paginationDto);
    Task CreatePlanAsync(CreditPlanCreateDto planCreateDto);
    Task<PaginationList<CreditPlanDto>> GetPlansAsync(PaginationDto paginationDto);
    Task<CreditShcheduleDto> GetScheduleAsync(int creditId);
    Task CloseCreditAsync(int creditId);
    Task PayPercentsAsync(int creditId);
}
