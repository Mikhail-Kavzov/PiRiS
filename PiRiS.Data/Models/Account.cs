using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Data.Models;

[Index(nameof(AccountNumber),IsUnique = true)]
public class Account
{
    public int AccountId { get; set; }

    [StringLength(13, MinimumLength = 13)]
    public string AccountNumber { get; set; } = null!;

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public decimal Balance { get; set; }

    public int AccountPlanId { get; set; }

    public AccountPlan AccountPlan { get; set; } = null!;

    public List<Credit> MainCredits { get; set; } = null!;

    public List<Credit> PercentCredits { get; set; } = null!;

    public List<Deposit> MainDeposits { get; set; } = null!;

    public List<Deposit> PercentDeposits { get; set; } = null!;

}
