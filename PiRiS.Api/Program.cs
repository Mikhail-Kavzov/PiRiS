using PiRiS.Api.Extensions;
using PiRiS.Data.Extensions;
using PiRiS.Business.Extensions;
using PiRiS.Business.Mapper.Config;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

internal class Program
{
    static void SetupMvcNewtonsoftJson(MvcNewtonsoftJsonOptions options)
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson(SetupMvcNewtonsoftJson);

        builder.Services.AddOpenApiDocument();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddEntityFramework(builder.Configuration);

        builder.Services.AddBusinessDependencies(builder.Configuration);
        builder.Services.AddBusinessMappings();
        builder.Services.AddCors(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            // Add OpenAPI 3.0 document serving middleware
            // Available at: http://localhost:<port>/swagger/v1/swagger.json
            app.UseOpenApi();

            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();
        app.UseCors("BankPolicy");
        app.UseAuthorization();
        app.UseErrorHandling();

        app.MapControllers();

        app.Run();
    }
}