using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Dto.CreditPlan;

public class CreditPlanAgreementDto
{
    public int CreditPlanId { get; set; }

    public string Name { get; set; } = null!;

    public int MonthPeriod { get; set; }

    public double Percent { get; set; }

    public string CreditType { get; set; }

    public string CurrencyName { get; set; }
}
