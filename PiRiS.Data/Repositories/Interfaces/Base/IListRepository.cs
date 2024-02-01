using System.Linq.Expressions;

namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface IListRepository<T>
{
    Task<IEnumerable<T>> GetListAsync(int skip, int take, Expression<Func<T,bool>>? predicate = null,Expression<Func<T, object>>? sort = null, bool isAscending = true);
    Task<IEnumerable<T>> GetAllAsync();
}
