using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;
using System.Linq.Expressions;

namespace PiRiS.Data.Repositories.Interfaces;

public interface ICreditRepository : IRepository, ICreateRepository<Credit>, IListRepository<Credit>,
    ICountRepository<Credit>, IExistsRepository<Credit>, IGetRepository<Credit, int>, IUpdateRepository<Credit>
{
    Task<string> GetCurrencyNameAsync(Expression<Func<Credit, bool>> predicate);
}
