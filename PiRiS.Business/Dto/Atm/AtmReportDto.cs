namespace PiRiS.Business.Dto.Atm;

public class AtmReportDto
{
    public string CreditCardNumber { get; set; }
    public DateTime OperationDate { get; set; }
    public decimal Sum { get; set; }
    public string OperationName { get; set; }
    public string CurrencyName { get; set; }
}
