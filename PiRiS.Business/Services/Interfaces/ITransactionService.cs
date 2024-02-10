using PiRiS.Data.Models;

namespace PiRiS.Business.Services.Interfaces;

public interface ITransactionService: IBaseService
{
    Task PerformTransactionAsync(Account debitAccount, Account creditAccount, decimal amount);
    Task PerformTransactionAsync(int debitAccountId, int creditAccountId, decimal amount);
    Task WithdrawBankTransactionAsync(decimal amount);
    Task PerformBankDebitTransactionAsync(decimal amount);
    Task CloseDepositsForDayAsync();
    Task CloseCreditsForDayAsync();
}
