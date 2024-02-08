using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiRiS.Data.Context;
using PiRiS.Data.Repositories;
using PiRiS.Data.Repositories.Interfaces;
using PiRiS.Data.UnitOfWork;
using UoW = PiRiS.Data.UnitOfWork.UnitOfWork;

namespace PiRiS.Data.Extensions;

public static class ServiceExtensions
{
    public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BankDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IDisabilityRepository, DisabilityRepository>();
        services.AddTransient<ICitizenshipRepository, CitizenshipRepository>();
        services.AddTransient<ICityRepository, CityRepository>();
        services.AddTransient<IFamilyStatusRepository, FamilyStatusRepository>();
        services.AddTransient<IDepositPlanRepository, DepositPlanRepository>();
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        services.AddTransient<IDepositRepository, DepositRepository>();
        services.AddTransient<ICreditPlanRepository, CreditPlanRepository>();
        services.AddTransient<ICreditRepository, CreditRepository>();
        services.AddTransient<IAccountPlanRepository, AccountPlanRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IBankInformationRepository, BankInformationRepository>();

        services.AddTransient<IUnitOfWork, UoW>();
    }
}
