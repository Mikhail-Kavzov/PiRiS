namespace PiRiS.Data.Repositories.Interfaces.Base;

public interface IRepository
{
    Task SaveChangesAsync();
    void ClearContext();
}
