using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Dto.DepositPlan;

public class DepositPlanAggreementDto
{
    public int DepositPlanId { get; set; }

    public string Name { get; set; }

    public int DayPeriod { get; set; }

    public double Percent { get; set; }

    public DepositType DepositType { get; set; }

}
