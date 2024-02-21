using PiRiS.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.Deposit;

public class DepositCreateDto
{
    [Required]
    public int DepositPlanId { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    [RegularExpression(Patterns.DepositNumber, ErrorMessage = "Deposit number contains 9 numbers")]
    public string DepositNumber { get; set; }

    [Required]
    [Range(1, BankParams.MaxCurrencyValue)]
    public decimal Sum { get; set; }

}
