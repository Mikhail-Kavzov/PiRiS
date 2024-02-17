namespace PiRiS.Business.Dto.Credit;

public class CreditScheduleDto
{
    public int CreditId { get; set; }

    public DateTime CurrentDay { get; set; }

    public Dictionary<DateTime, double> Schedule { get; set; }

    public string CurrencyName { get; set; }
}
