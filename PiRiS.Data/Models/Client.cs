namespace PiRiS.Data.Models;

public class Client
{
    public int ClientId { get; set; }

    public string Surname { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string PassportSeries { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string IssuedBy { get; set; } = null!;

    public DateTime DateOfIssue { get; set; }

    public string IdentificationNumber { get; set; } = null!;

    public string PlaceOfBirth { get; set; } = null!;

    public string LocationAddress { get; set; } = null!;

    public int CityId { get; set; }
    public City City { get; set; } = null!;

    public string? HomePhone { get; set; }

    public string? MobilePhone { get; set; }

    public string? Email { get; set; }

    public string? Company { get; set; }

    public string? JobTitle { get; set; }

    public string RegistrationAddress { get; set; } = null!;

    public int CitizenshipId { get; set; }
    public Citizenship Citizenship { get; set; } = null!;

    public int DisabilityId { get; set; }
    public Disability Disability { get; set; } = null!;

    public int FamilyStatusId { get; set; }
    public FamilyStatus FamilyStatus { get; set; } = null!;

    public bool IsPensioner { get; set; }

    public decimal? MonthIncome { get; set; }

    public int? CurrencyId { get; set; }
    public Currency? Currency { get; set; }
}
