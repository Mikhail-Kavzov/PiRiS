using System.Linq.Expressions;

namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface IGetRepository<T, TKey>
{
    Task<T?> GetEntityAsync(TKey id, bool trackChanges = true);
    Task<T?> GetEntityAsync(Expression<Func<T, bool>> predicate);
}
