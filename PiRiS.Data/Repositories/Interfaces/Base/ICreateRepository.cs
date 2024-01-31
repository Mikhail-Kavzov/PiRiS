namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface ICreateRepository<T>
{
    void Create(T entity);
}
