using PiRiS.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto;

public class CreditCreateDto
{
    [Required]
    public int CurrencyId { get; set; }

    [Required]
    public int CreditPlanId { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    [RegularExpression(Patterns.CreditNumber, ErrorMessage = "Deposit number contains 9 numbers")]
    public string CreditNumber { get; set; }

    [Range(100, double.MaxValue, ErrorMessage = "Credit sum shouldn't be less than {0}")]
    public decimal Sum { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
}
