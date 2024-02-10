using System.ComponentModel.DataAnnotations;

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

    [StringLength(16, MinimumLength = 16)]
    public string CreditCardNumber { get; set; } = null!;

    [StringLength(4, MinimumLength = 4)]
    public string CreditCardCode { get; set; } = null!;

}
