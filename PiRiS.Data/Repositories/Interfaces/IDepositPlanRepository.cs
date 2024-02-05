using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface IDepositPlanRepository: IListRepository<DepositPlan>, IRepository, IExistsRepository<DepositPlan>, IGetRepository<DepositPlan, int>
{
}
