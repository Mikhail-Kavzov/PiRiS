namespace PiRiS.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var origins = (configuration.GetSection("AllowedOrigins").Get<string>() ?? string.Empty).Split(',');
        services.AddCors(opt => opt.AddPolicy("BankPolicy", builder =>
        {
            builder.WithOrigins(origins);
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowCredentials();
        }
        ));
    }
}
