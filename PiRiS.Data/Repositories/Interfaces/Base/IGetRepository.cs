namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface IGetRepository<T, TKey>
{
    Task<T?> GetEntityAsync(TKey id, bool trackChanges = true);
}
