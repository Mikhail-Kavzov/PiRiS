using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Dto.Credit;

public class CreditDto
{
    public int CreditId { get; set; }

    public string CreditNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Sum { get; set; }

    public string PlanName { get; set; }

    public double Percent { get; set; }

    public CreditType CreditType { get; set; }

    public string MainAccountNumber { get; set; }

    public string PercentAccountNumber { get; set; }
}
