using PiRiS.Data.Repositories.Interfaces;

namespace PiRiS.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IClientRepository ClientRepository { get; }

    public UnitOfWork(IClientRepository clientRepository)
    {
        ClientRepository = clientRepository;
    }
}
