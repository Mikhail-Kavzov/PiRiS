using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface ICurrencyRepository : IListRepository<Currency>, IRepository, IExistsRepository<Currency>
{
}
