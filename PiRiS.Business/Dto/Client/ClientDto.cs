using PiRiS.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.Client;

public class ClientDto
{
    public int? ClientId { get; set; }

    [Required]
    [RegularExpression(Patterns.ClientName, ErrorMessage = "Only latin or cyrillic letters up to 30 symbols")]
    public string Surname { get; set; }

    [Required]
    [RegularExpression(Patterns.ClientName, ErrorMessage = "Only latin or cyrillic letters up to 30 symbols")]
    public string FirstName { get; set; }

    [Required]
    [RegularExpression(Patterns.ClientName, ErrorMessage = "Only latin or cyrillic letters up to 30 symbols")]
    public string LastName { get; set; }

    [Required(ErrorMessage = $"{nameof(DateOfBirth)} is required")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [RegularExpression(Patterns.PassportSeries, ErrorMessage = "Enter valid passport series")]
    public string PassportSeries { get; set; }

    [Required]
    [RegularExpression(Patterns.PassportNumber, ErrorMessage = "Enter valid passport number")]
    public string PassportNumber { get; set; }

    [Required(ErrorMessage = $"{nameof(IssuedBy)} is required")]
    [MaxLength(100, ErrorMessage = $"{nameof(IssuedBy)} shoudn't be more than 100 symbols")]
    public string IssuedBy { get; set; }

    [Required(ErrorMessage = $"{nameof(DateOfIssue)} is required")]
    public DateTime DateOfIssue { get; set; }

    [Required]
    [RegularExpression(Patterns.IdentificationNumber, ErrorMessage = "Only latin letters or figures. Length 14 symbols")]
    public string IdentificationNumber { get; set; }

    [Required(ErrorMessage = $"{nameof(PlaceOfBirth)} is required")]
    [MaxLength(100, ErrorMessage = $"{nameof(PlaceOfBirth)} shoudn't be more than 100 symbols")]
    public string PlaceOfBirth { get; set; }

    [Required(ErrorMessage = $"{nameof(LocationAddress)} is required")]
    [MaxLength(100, ErrorMessage = $"{nameof(LocationAddress)} shoudn't be more than 100 symbols")]
    public string LocationAddress { get; set; }

    [Required(ErrorMessage = $"{nameof(CityId)} is required")]
    public int CityId { get; set; }

    [RegularExpression(Patterns.Phone, ErrorMessage = "Enter valid home phone number")]
    [MaxLength(13, ErrorMessage = $"{nameof(HomePhone)} shoudn't be more than 13 symbols")]
    public string HomePhone { get; set; }

    [RegularExpression(Patterns.Phone, ErrorMessage = "Enter valid mobile phone number")]
    [MaxLength(13, ErrorMessage = $"{nameof(MobilePhone)} shoudn't be more than 13 symbols")]
    public string MobilePhone { get; set; }

    [RegularExpression(Patterns.Email, ErrorMessage = "Enter valid email address")]
    [MaxLength(50, ErrorMessage = $"{nameof(Email)} shoudn't be more than 50 symbols")]
    public string Email { get; set; }

    [MaxLength(50, ErrorMessage = $"{nameof(Company)} shoudn't be more than 50 symbols")]
    public string Company { get; set; }

    [MaxLength(50, ErrorMessage = $"{nameof(JobTitle)} shoudn't be more than 50 symbols")]
    public string JobTitle { get; set; }

    [Required(ErrorMessage = $"{nameof(RegistrationAddress)} is required")]
    [MaxLength(100, ErrorMessage = $"{nameof(RegistrationAddress)} shoudn't be more than 100 symbols")]
    public string RegistrationAddress { get; set; }

    [Required(ErrorMessage = $"{nameof(CitizenshipId)} is required")]
    public int CitizenshipId { get; set; }

    [Required(ErrorMessage = $"{nameof(DisabilityId)} is required")]
    public int DisabilityId { get; set; }

    [Required(ErrorMessage = $"{nameof(FamilyStatusId)} is required")]
    public int FamilyStatusId { get; set; }

    public bool IsPensioner { get; set; }

    [Range(0, BankParams.MaxCurrencyValue, ErrorMessage = "Month income should be >=0")]
    public decimal? MonthIncome { get; set; }
}
