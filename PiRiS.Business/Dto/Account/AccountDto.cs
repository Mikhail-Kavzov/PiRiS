namespace PiRiS.Business.Dto.Account;

public class AccountDto
{
    public int AccountId { get; set; }

    public string AccountNumber { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public decimal Balance { get; set; }

    public string AccountPlanCode { get; set; }

    public string AccountPlanName { get; set; }

    public string AccountPlanType { get; set; }
}
