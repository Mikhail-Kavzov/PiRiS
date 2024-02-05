using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Dto.Deposit;

public class DepositDto
{
    public int DepositId { get; set; }

    public string DepositNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Sum { get; set; }

    public string PlanName { get; set; }

    public double Percent { get; set; }

    public DepositType DepositType { get; set; }

    public string MainAccountNumber { get; set; }

    public string PercentAccountNumber { get; set; }
}
