using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiRiS.Business.Managers;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Options;
using PiRiS.Business.Services;
using PiRiS.Business.Services.Interfaces;

namespace PiRiS.Business.Extensions;

public static class ServiceExtensions
{
    public static void AddBusinessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IBankService, BankService>();
        services.AddTransient<ITransactionService, TransactionService>();

        services.AddTransient<IClientManager, ClientManager>();
        services.AddTransient<IDepositManager, DepositManager>();
        services.AddTransient<ICreditManager, CreditManager>();
        services.AddTransient<ICurrencyManager, CurrencyManager>();
        services.AddTransient<IBankManager, BankManager>();
        services.AddTransient<IAtmManager, AtmManager>();
        services.AddTransient<IAccountManager, AccountManager>();

        
        services.Configure<CurrencyOptions>(configuration.GetSection("Currencies"));
    }
}
