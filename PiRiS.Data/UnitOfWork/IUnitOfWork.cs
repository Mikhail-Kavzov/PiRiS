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

        IDepositPlanRepository DepositPlanRepository { get; }

        ICurrencyRepository CurrencyRepository { get; }

        IDepositRepository DepositRepository { get; }

        ICreditPlanRepository CreditPlanRepository { get; }

        ICreditRepository CreditRepository { get; }

        IAccountPlanRepository AccountPlanRepository { get; }

        ITransactionRepository TransactionRepository { get; }

        IAccountRepository AccountRepository { get; }

        IBankInformationRepository BankInformationRepository { get; }
    }
}
