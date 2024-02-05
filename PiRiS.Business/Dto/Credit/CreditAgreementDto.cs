using PiRiS.Business.Dto.CreditPlan;
using PiRiS.Business.Dto.Currency;

namespace PiRiS.Business.Dto.Credit;

public class CreditAgreementDto
{
    public List<CreditPlanAgreementDto> CreditPlans { get; set; }
    public List<CurrencyDto> Currencies { get; set; }
}
