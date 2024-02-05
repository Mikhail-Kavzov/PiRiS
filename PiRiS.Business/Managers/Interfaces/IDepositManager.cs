using PiRiS.Business.Dto;

namespace PiRiS.Business.Managers.Interfaces;

public interface IDepositManager : IBaseManager
{
    Task<DepositAgreementDto> GetDepositAgreementAsync();
    Task CreateDepositAsync(DepositCreateDto depositCreateDto);
    Task<PaginationList<DepositDto>> GetDepositsAsync(DepositPaginationDto paginationDto);
}
