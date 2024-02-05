using PiRiS.Business.Dto.Currency;
using PiRiS.Business.Dto.DepositPlan;

namespace PiRiS.Business.Dto.Deposit;

public class DepositAgreementDto
{
    public List<DepositPlanAggreementDto> DepositPlans { get; set; }
    public List<CurrencyDto> Currencies { get; set; }
}
