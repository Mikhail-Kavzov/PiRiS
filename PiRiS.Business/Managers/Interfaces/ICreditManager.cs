using PiRiS.Business.Dto;

namespace PiRiS.Business.Managers.Interfaces;

public interface ICreditManager : IBaseManager
{
    Task<CreditAgreementDto> GetCreditAgreementAsync();
    Task CreateCreditAsync(CreditCreateDto creditCreateDto);
}
