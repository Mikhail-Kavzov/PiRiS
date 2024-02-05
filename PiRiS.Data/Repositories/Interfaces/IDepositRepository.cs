using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface IDepositRepository : IRepository, ICreateRepository<Deposit>,
    IListRepository<Deposit>, ICountRepository<Deposit>, IExistsRepository<Deposit>
{
}
