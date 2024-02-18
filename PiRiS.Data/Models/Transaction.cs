namespace PiRiS.Data.Models;

public class Transaction
{
    public int TransactionId { get; set; }

    public int DebitAccountId { get; set; }

    public int CreditAccountId { get; set; }

    public DateTime TransactionDay { get; set; }

    public decimal Amount { get; set; }

    public Account DebitAccount { get; set; } = null!;

    public Account CreditAccount { get; set; } = null!;
}
