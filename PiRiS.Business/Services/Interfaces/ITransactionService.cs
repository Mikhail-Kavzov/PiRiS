using PiRiS.Data.Models;
using PiRiS.Data.Models.Enums;

namespace PiRiS.Business.Services.Interfaces;

public interface ITransactionService: IBaseService
{
    Task PerformTransactionAsync(Account debitAccount, Account creditAccount, decimal amount, AccountType debitType, AccountType creditType);
    Task PerformTransactionAsync(int debitAccountId, int creditAccountId, decimal amount);
    Task WithdrawBankTransactionAsync(decimal amount);
    Task PerformBankIncomeTransactionAsync(decimal amount);
    Task CloseDepositsForDayAsync();
    Task CloseCreditsForDayAsync();
}
