using PiRiS.Common.Constants;
using PiRiS.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.CreditPlan;

public class CreditPlanCreateDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [Range(1, BankParams.MaxPeriodValue)]
    public int MonthPeriod { get; set; }

    [Required]
    public int CurrencyId { get; set; }

    [Required]
    [Range(0, BankParams.MaxPercentValue)]
    public double Percent { get; set; }

    [Required]
    public CreditType CreditType { get; set; }
}
