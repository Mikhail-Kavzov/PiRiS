namespace PiRiS.Business.Dto.Transaction;

public class TransactionDto
{
    public int TransactionId { get; set; }

    public string DebitAccountNumber { get; set; }

    public string CreditAccountNumber { get; set; }

    public DateTime TransactionDay { get; set; }

    public decimal Amount { get; set; }
}
