using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface ICreditPlanRepository : IRepository, IGetRepository<CreditPlan, int>, 
    IListRepository<CreditPlan>, ICountRepository<CreditPlan>, ICreateRepository<CreditPlan>
{
}
