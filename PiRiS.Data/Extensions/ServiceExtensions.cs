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
        });

        services.AddScoped<IClientRepository, ClientRepository>();

        services.AddScoped<IUnitOfWork, UoW>();
    }
}
