using Microsoft.Extensions.DependencyInjection;
using PiRiS.Business.Managers;
using PiRiS.Business.Managers.Interfaces;

namespace PiRiS.Business.Extensions;

public static class ServiceExtensions
{
    public static void AddBusinessDependencies(this IServiceCollection services)
    {
        services.AddTransient<IClientManager, ClientManager>();
        services.AddTransient<IDepositManager, DepositManager>();
        services.AddTransient<ICreditManager, CreditManager>();
    }
}
