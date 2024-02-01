using PiRiS.Data.Repositories.Interfaces;

namespace PiRiS.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IClientRepository ClientRepository { get; }

    public IDisabilityRepository DisabilityRepository { get; }

    public IFamilyStatusRepository FamilyStatusRepository { get; }

    public ICitizenshipRepository CitizenshipRepository { get; }

    public ICityRepository CityRepository { get; }

    public UnitOfWork(IClientRepository clientRepository, IDisabilityRepository disabilityRepository,
        IFamilyStatusRepository familyStatusRepository, ICityRepository cityRepository,
        ICitizenshipRepository citizenshipRepository)
    {
        ClientRepository = clientRepository;
        DisabilityRepository = disabilityRepository;
        FamilyStatusRepository = familyStatusRepository;
        CityRepository = cityRepository;
        CitizenshipRepository = citizenshipRepository;
    }
}
