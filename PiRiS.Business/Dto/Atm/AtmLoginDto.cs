using PiRiS.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.Atm;

public class AtmLoginDto
{
    [Required]
    [RegularExpression(Patterns.CreditCardNumber)]
    public string CreditCardNumber { get; set; }

    [Required]
    [RegularExpression(Patterns.CreditCardCode)]
    public string CreditCardCode { get; set; }
}
