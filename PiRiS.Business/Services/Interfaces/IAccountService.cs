using PiRiS.Data.Models;

namespace PiRiS.Business.Services.Interfaces;

public interface IAccountService
{
    Task CreateAccountsAsync(Deposit deposit);
    Task CreateAccountsAsync(Credit credit);
}
