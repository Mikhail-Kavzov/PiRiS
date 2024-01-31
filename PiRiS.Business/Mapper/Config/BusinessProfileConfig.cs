using Microsoft.Extensions.DependencyInjection;

namespace PiRiS.Business.Mapper.Config;

public static class BusinessProfileConfig
{
    public static void AddBusinessMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(x =>
        {
            x.AddProfile<BusinessProfile>();
        });
    }
}
