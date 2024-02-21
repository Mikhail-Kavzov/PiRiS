using PiRiS.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.Atm;

public class WithdrawMoneyDto
{
    [Required]
    [RegularExpression(Patterns.CreditCardNumber)]
    public string CreditCardNumber { get; set; }

    [Required]
    [RegularExpression(Patterns.CreditCardCode)]
    public string CreditCardCode { get; set; }


    [Required]
    [Range(0, BankParams.MaxCurrencyValue)]
    public decimal Sum { get; set; }
}
