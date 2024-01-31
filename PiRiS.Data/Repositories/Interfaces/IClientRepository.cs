using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface IClientRepository : IRepository, IUpdateRepository<Client>,
    IDeleteRepository<Client>, IGetRepository<Client, int>, 
    ICreateRepository<Client>, IListRepository<Client>
{

}
