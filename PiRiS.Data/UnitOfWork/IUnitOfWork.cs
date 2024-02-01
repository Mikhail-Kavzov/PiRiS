using PiRiS.Data.Repositories.Interfaces;

namespace PiRiS.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get; }

        IDisabilityRepository DisabilityRepository { get; }

        IFamilyStatusRepository FamilyStatusRepository { get; }

        ICitizenshipRepository CitizenshipRepository { get; }

        ICityRepository CityRepository { get; }
    }
}
