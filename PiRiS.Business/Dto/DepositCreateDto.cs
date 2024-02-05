using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto;

public class DepositCreateDto
{
    [Required]
    public int CurrencyId { get; set; }

    [Required]
    public int DepositPlanId { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    [RegularExpression(@"\d{9}", ErrorMessage = "Deposit number contains 9 numbers")]
    public string DepositNumber { get; set; }

    [Required]
    [Range(1, double.MaxValue)]
    public decimal Sum { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

}
