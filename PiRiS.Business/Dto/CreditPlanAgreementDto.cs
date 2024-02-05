using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Dto;

public class CreditPlanAgreementDto
{
    public int CreditPlanId { get; set; }

    public string Name { get; set; } = null!;

    public int MonthPeriod { get; set; }

    public double Percent { get; set; }

    public CreditType CreditType { get; set; }
}
