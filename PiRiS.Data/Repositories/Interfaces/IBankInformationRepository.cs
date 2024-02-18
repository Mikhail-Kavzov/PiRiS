using PiRiS.Data.Models;
using PiRiS.Data.Repositories.Interfaces.Base;

namespace PiRiS.Data.Repositories.Interfaces;

public interface IBankInformationRepository : ICreateRepository<BankInformation>, IUpdateRepository<BankInformation>,
    IGetRepository<BankInformation,int>, IRepository
{
}
