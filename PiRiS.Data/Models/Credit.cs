namespace PiRiS.Data.Models;

public class Credit
{
    public int CreditId { get; set; }

    public string CreditNumber { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Sum { get; set; }

    public int CreditPlanId { get; set; }

    public CreditPlan CreditPlan { get; set; } = null!;

    public int MainAccountId { get; set; }

    public Account MainAccount { get; set; } = null!;

    public int PercentAccountId { get; set; }

    public Account PercentAccount { get; set; } = null!;

    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;

    public int CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;

}
