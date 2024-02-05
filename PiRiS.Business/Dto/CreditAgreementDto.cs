using PiRiS.Data.Models;

namespace PiRiS.Business.Dto;

public class CreditAgreementDto
{
    public List<CreditPlanAgreementDto> CreditPlans { get; set; }
    public List<CurrencyDto> Currencies { get; set; }
}
