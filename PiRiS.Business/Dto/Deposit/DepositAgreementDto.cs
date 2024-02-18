using PiRiS.Business.Dto.Currency;
using PiRiS.Business.Dto.DepositPlan;

namespace PiRiS.Business.Dto.Deposit;

public class DepositAgreementDto
{
    public List<DepositPlanAgreementDto> DepositPlans { get; set; }
}
