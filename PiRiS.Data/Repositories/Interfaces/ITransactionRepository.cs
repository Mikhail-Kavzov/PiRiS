using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface ITransactionRepository : IRepository, IGetRepository<Transaction,int>, 
    ICreateRepository<Transaction>, IListRepository<Transaction>, ICountRepository<Transaction>
{
}
