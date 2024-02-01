namespace PiRiS.Business.Dto;

public class ClientAdditionalsDto
{
    public List<DisabilityDto> Disabilities { get; set; }
    public List<CitizenshipDto> Citizenships { get; set; }
    public List<CityDto> Cities { get; set; }
    public List<FamilyStatusDto> FamilyStatuses { get; set; }
}
