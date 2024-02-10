using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Dto.DepositPlan;

public class DepositPlanDto
{
    public int DepositPlanId { get; set; }

    public string Name { get; set; }

    public int CurrencyName { get; set; }

    public int DayPeriod { get; set; }

    public double Percent { get; set; }

    public string DepositType { get; set; }
}
