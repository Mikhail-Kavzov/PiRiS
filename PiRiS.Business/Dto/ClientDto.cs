using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto;

public class ClientDto
{
    public int? ClientId { get; set; }

    [RegularExpression("^[A-z,А-я,ё,Ё]{1,30}$", ErrorMessage = "Only latin or cyrillic letters up to 30 symbols")]
    public string Surname { get; set; }

    [RegularExpression("^[A-z,А-я,ё,Ё]{1,30}$", ErrorMessage = "Only latin or cyrillic letters up to 30 symbols")]
    public string FirstName { get; set; }

    [RegularExpression("^[A-z,А-я,ё,Ё]{1,30}$", ErrorMessage = "Only latin or cyrillic letters up to 30 symbols")]
    public string LastName { get; set; }

    [Required(ErrorMessage = $"{nameof(DateOfBirth)} is required")]
    public DateTime DateOfBirth { get; set; }

    [StringLength(2, MinimumLength = 2, ErrorMessage = $"{nameof(PassportSeries)} should be 2 symbols")]
    public string PassportSeries { get; set; }

    [StringLength(7, MinimumLength = 7, ErrorMessage = $"{nameof(PassportSeries)} should be 7 symbols")]
    public string PassportNumber { get; set; }

    [Required(ErrorMessage = $"{nameof(IssuedBy)} is required")]
    [MaxLength(100, ErrorMessage = $"{nameof(IssuedBy)} shoudn't be more than 100 symbols")]
    public string IssuedBy { get; set; }

    [Required(ErrorMessage = $"{nameof(DateOfIssue)} is required")]
    public DateTime DateOfIssue { get; set; }

    [RegularExpression("^[A-Z,0-9]{14}$", ErrorMessage = "Only latin letters or figures. Length 14 symbols")]
    public string IdentificationNumber { get; set; }

    [Required(ErrorMessage = $"{nameof(PlaceOfBirth)} is required")]
    [MaxLength(100, ErrorMessage = $"{nameof(PlaceOfBirth)} shoudn't be more than 100 symbols")]
    public string PlaceOfBirth { get; set; }

    [Required(ErrorMessage = $"{nameof(LocationAddress)} is required")]
    [MaxLength(100, ErrorMessage = $"{nameof(LocationAddress)} shoudn't be more than 100 symbols")]
    public string LocationAddress { get; set; }

    [Required(ErrorMessage = $"{nameof(CityId)} is required")]
    public int CityId { get; set; }

    [Phone]
    [MaxLength(15, ErrorMessage = $"{nameof(HomePhone)} shoudn't be more than 15 symbols")]
    public string HomePhone { get; set; }

    [Phone]
    [MaxLength(15, ErrorMessage = $"{nameof(HomePhone)} shoudn't be more than 15 symbols")]
    public string MobilePhone { get; set; }

    [EmailAddress]
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

    public decimal? MonthIncome { get; set; }
}
