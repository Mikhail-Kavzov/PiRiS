using PiRiS.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.CreditPlan;

public class CreditPlanCreateDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int MonthPeriod { get; set; }

    [Required]
    public int CurrencyId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double Percent { get; set; }

    [Required]
    public CreditType CreditType { get; set; }
}
