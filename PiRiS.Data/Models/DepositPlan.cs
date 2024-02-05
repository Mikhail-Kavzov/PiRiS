using PiRiS.Data.Models.Enums;

namespace PiRiS.Data.Models;

public class DepositPlan
{
    public int DepositPlanId { get; set; }

    public string Name { get; set; } = null!;

    public int DayPeriod { get; set; }

    public double Percent { get; set; }

    public DepositType DepositType { get; set; }

    public List<Deposit> Deposits { get; set; } = null!;

    public int MainAccountPlanId { get; set; }

    public AccountPlan MainAccountPlan { get; set; } = null!;

    public int PercentAccountPlanId { get; set; }

    public AccountPlan PercentAccountPlan { get; set; } = null!;
}
