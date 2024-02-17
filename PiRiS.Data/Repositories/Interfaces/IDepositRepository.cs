using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories.Interfaces;

public interface IDepositRepository : IRepository, ICreateRepository<Deposit>,
    IListRepository<Deposit>, ICountRepository<Deposit>, IExistsRepository<Deposit>, 
    IGetRepository<Deposit, int>, IUpdateRepository<Deposit>
{
    Task<string> GetCurrencyNameAsync(Expression<Func<Deposit,bool>> predicate);
}
