namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface IUpdateRepository<T>
{
    void Update(T entity);
}
