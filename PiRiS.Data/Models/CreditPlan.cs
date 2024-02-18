using PiRiS.Data.Models.Enums;

namespace PiRiS.Data.Models;

public class CreditPlan
{
    public int CreditPlanId { get; set; }

    public string Name { get; set; } = null!;

    public int MonthPeriod { get; set; }

    public int CurrencyId { get; set; }

    public Currency Currency { get; set; } = null!;

    public double Percent { get; set; }

    public CreditType CreditType { get; set; }

    public List<Credit> Credits { get; set; } = null!;

    public int MainAccountPlanId { get; set; }

    public AccountPlan MainAccountPlan { get; set; } = null!;

    public int PercentAccountPlanId { get; set; }

    public AccountPlan PercentAccountPlan { get; set; } = null!;
}
