using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiRiS.Business.Managers;
using PiRiS.Business.Managers.Interfaces;
using PiRiS.Business.Services;
using PiRiS.Business.Services.Interfaces;

namespace PiRiS.Business.Extensions;

public static class ServiceExtensions
{
    public static void AddBusinessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ITransactionService, TransactionService>();
        services.AddTransient<IBankService, BankService>();

        services.AddTransient<IClientManager, ClientManager>();
        services.AddTransient<IDepositManager, DepositManager>();
        services.AddTransient<ICreditManager, CreditManager>();
    }
}
