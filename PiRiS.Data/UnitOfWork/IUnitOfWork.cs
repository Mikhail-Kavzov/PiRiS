using PiRiS.Data.Repositories.Interfaces;

namespace PiRiS.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get; }
    }
}
