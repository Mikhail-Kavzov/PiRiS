namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface IDeleteRepository<T>
{
    void Delete(T entity);
}
