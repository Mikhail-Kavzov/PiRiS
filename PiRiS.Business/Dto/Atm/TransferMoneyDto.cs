using PiRiS.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.Atm;

public class TransferMoneyDto
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

    [Required]
    [RegularExpression(Patterns.Phone)]
    public string MobilePhone { get; set; }

    [Required]
    [RegularExpression(Patterns.Phone)]
    [Compare(nameof(MobilePhone), ErrorMessage = "Phones must match")]
    public string MobilePhoneConfirmation { get; set; }
}
