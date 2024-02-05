namespace PiRiS.Business.Dto;

public class DepositAgreementDto
{
    public List<DepositPlanAggreementDto> DepositPlans { get; set; }
    public List<CurrencyDto> Currencies { get; set; }
}
