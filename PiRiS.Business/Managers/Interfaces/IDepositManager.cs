using PiRiS.Business.Dto;
using PiRiS.Business.Dto.Deposit;
using PiRiS.Business.Dto.DepositPlan;

namespace PiRiS.Business.Managers.Interfaces;

public interface IDepositManager : IBaseManager
{
    Task<DepositAgreementDto> GetDepositAgreementAsync();
    Task CreateDepositAsync(DepositCreateDto depositCreateDto);
    Task<PaginationList<DepositDto>> GetDepositsAsync(DepositPaginationDto paginationDto);
    Task CreatePlanAsync(DepositPlanCreateDto depositPlanCreateDto);
    Task<PaginationList<DepositPlanDto>> GetPlansAsync(PaginationDto paginationDto);
    Task WithdrawPercentsAsync(int depositId);
    Task CloseDepositAsync(int depositId);
}
