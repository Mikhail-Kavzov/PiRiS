using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Dto.Deposit;

public class DepositDto
{
    public int DepositId { get; set; }

    public int DepositPlanId { get; set; }

    public int ClientId { get; set; }

    public string DepositNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Sum { get; set; }

    public string CurrencyName { get; set; }

    public string PlanName { get; set; }

    public double Percent { get; set; }

    public string DepositType { get; set; }

    public string MainAccountNumber { get; set; }

    public string PercentAccountNumber { get; set; }

    public string Surname { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public decimal DailyProfit { get; set; }

    public bool CanClose { get; set; }

    public bool CanWithdraw { get; set; }
}
