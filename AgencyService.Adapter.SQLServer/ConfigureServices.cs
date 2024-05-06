using AgencyService.Adapter.SQLServer.Interfaces;
using AgencyService.Adapter.SQLServer.Persistance;
using AgencyService.Adapter.SQLServer.Persistance.Interceptors;
using AgencyService.Adapter.SQLServer.Repositories;
using AgencyService.Core.Application.Ports.Driven;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using LinqKit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Common;

namespace AgencyService.Adapter.SQLServer;
public static class ConfigureServices
{
    public static WebApplicationBuilder AddSqlServerServices(this WebApplicationBuilder builder)
    {
        //builder.Configuration.AddAndConfigureSecretManager(builder.Environment, RegionEndpoint.EUNorth1);

        var connectionString = builder.Configuration.GetConnectionString("AgencyServiceDatabase");

        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = builder.BuildConnectionStringFromUserSecrets();
        }

        builder.Services.AddDbContext<AgencyServiceDbContext>(options =>
                options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(AgencyServiceDbContext).Assembly.FullName)));

        builder.Services.AddScoped<AgencyServiceDbContextInitialiser>();
        builder.Services.AddScoped<BaseAuditableEntitySaveChangesInterceptor>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.RegisterRepositories();

        builder.Services.AddScoped<IAgencyServiceDbContext, AgencyServiceDbContext>();

        return builder;
    }

    public static async Task<WebApplication> InitializeDatabase(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<AgencyServiceDbContextInitialiser>();
                await initialiser.InitialiseAsync();
                await initialiser.SeedAsync();
            }
        }

        return app;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITravelAgencyRepository, TravelAgencyRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();

        return services;
    }

    private static string BuildConnectionStringFromUserSecrets(this WebApplicationBuilder builder)
    {
        var connectionStringBuilder = new DbConnectionStringBuilder();

        builder.Configuration
             .GetRequiredSection("Database")
             .GetChildren()
             .Where(x => !string.IsNullOrWhiteSpace(x.Value))
             .ForEach(x => connectionStringBuilder.Add(x.Key, x.Value!));

        return connectionStringBuilder.ToString();
    }
}
