using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface IAccountPlanRepository : IRepository, ICreateRepository<AccountPlan>,
    IGetRepository<AccountPlan,int>, IExistsRepository<AccountPlan>
{
}
