using System.Linq.Expressions;

namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface ICountRepository<T>
{
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
}
