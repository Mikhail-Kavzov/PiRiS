using System.Linq.Expressions;

namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface IExistsRepository<T>
{
    Task<bool> ExistsAsync(Expression<Func<T,bool>> predicate);
}
