using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Dto.CreditPlan;

public class CreditPlanDto
{
    public int CreditPlanId { get; set; }

    public string Name { get; set; }

    public int MonthPeriod { get; set; }

    public string CurrencyName { get; set; }

    public double Percent { get; set; }

    public CreditType CreditType { get; set; }
}
