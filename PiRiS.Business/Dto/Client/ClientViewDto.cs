namespace PiRiS.Business.Dto.Client;

public class ClientViewDto
{
    public int? ClientId { get; set; }

    public string Surname { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string PassportSeries { get; set; }

    public string PassportNumber { get; set; }

    public string IssuedBy { get; set; }

    public DateTime DateOfIssue { get; set; }

    public string IdentificationNumber { get; set; }

    public string PlaceOfBirth { get; set; }

    public string LocationAddress { get; set; }

    public string CityName { get; set; }

    public string HomePhone { get; set; }

    public string MobilePhone { get; set; }

    public string Email { get; set; }

    public string Company { get; set; }

    public string JobTitle { get; set; }

    public string RegistrationAddress { get; set; }

    public string CitizenshipName { get; set; }

    public string DisabilityStatus { get; set; }

    public string FamilyStatus { get; set; }

    public bool IsPensioner { get; set; }

    public decimal? MonthIncome { get; set; }
}
