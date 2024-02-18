using Microsoft.EntityFrameworkCore;
using PiRiS.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Data.Models;

[Index(nameof(Code), IsUnique = true)]
public class AccountPlan
{
    public int AccountPlanId { get; set; }

    [StringLength(4, MinimumLength = 4)]
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public AccountType AccountType { get; set; }

    public List<Account> Accounts { get; set; } = null!;

    public List<DepositPlan> MainDepositPlans { get; set; } = null!;

    public List<DepositPlan> PercentDepositPlans { get; set; } = null!;

    public List<CreditPlan> MainCreditPlans { get; set; } = null!;

    public List<CreditPlan> PercentCreditPlans { get;  set; } = null!;

}
