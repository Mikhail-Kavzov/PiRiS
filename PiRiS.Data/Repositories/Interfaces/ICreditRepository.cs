using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface ICreditRepository : IRepository, ICreateRepository<Credit>, IListRepository<Credit>, ICountRepository<Credit>
{
}
