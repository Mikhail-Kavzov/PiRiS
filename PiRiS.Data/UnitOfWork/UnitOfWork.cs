using PiRiS.Data.Repositories.Interfaces;

namespace PiRiS.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IClientRepository ClientRepository { get; }

    public IDisabilityRepository DisabilityRepository { get; }

    public IFamilyStatusRepository FamilyStatusRepository { get; }

    public ICitizenshipRepository CitizenshipRepository { get; }

    public ICityRepository CityRepository { get; }

    public IDepositPlanRepository DepositPlanRepository { get; }

    public ICurrencyRepository CurrencyRepository { get; }

    public IDepositRepository DepositRepository { get; }

    public ICreditPlanRepository CreditPlanRepository { get; }

    public ICreditRepository CreditRepository { get; }

    public IAccountPlanRepository AccountPlanRepository { get; }

    public UnitOfWork(IClientRepository clientRepository, IDisabilityRepository disabilityRepository,
        IFamilyStatusRepository familyStatusRepository, ICityRepository cityRepository,
        ICitizenshipRepository citizenshipRepository,
        IDepositPlanRepository depositPlanRepository, ICurrencyRepository currencyRepository,
        IDepositRepository depositRepository, ICreditPlanRepository creditPlanRepository,
        ICreditRepository creditRepository, IAccountPlanRepository accountPlanRepository)
    {
        ClientRepository = clientRepository;
        DisabilityRepository = disabilityRepository;
        FamilyStatusRepository = familyStatusRepository;
        CityRepository = cityRepository;
        CurrencyRepository = currencyRepository;
        DepositPlanRepository = depositPlanRepository;
        CitizenshipRepository = citizenshipRepository;
        DepositRepository = depositRepository;
        CreditPlanRepository = creditPlanRepository;
        CreditRepository = creditRepository;
        AccountPlanRepository = accountPlanRepository;
    }
}
