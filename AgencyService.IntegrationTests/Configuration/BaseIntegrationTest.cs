using AgencyService.Adapter.SQLServer.IntegrationTests.Enums;
using AgencyService.IntegrationTests.Interfaces;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockServerClientNet;
using Respawn;

namespace AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;

[Collection(CollectionDefinitions.IntergrationTestCollection)]
public abstract class BaseIntegrationTest<TDbContext> : IDisposable
    where TDbContext : DbContext
{
    protected TestServer TestServer { get; set; } = null!;
    protected IFixture Fixture { get; private set; }

    private Respawner _respawner = null!;


    protected BaseIntegrationTest()
    {
        Fixture = new Fixture();
    }

    protected static async Task ResetAndInitExpectations()
    {
        MockServerClient mockServerClient = new("localhost", 1090);

        await mockServerClient.ResetAsync();

        //  await RoutesExpectation.SetExpectations(mockServerClient);
    }

    protected async Task InitializeDatabaseAsync(IPrepopulateStrategy<TDbContext>? strategy = null)
    {
        IServiceProvider serviceProvider = TestServer.Services;

        using IServiceScope scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetService<TDbContext>() ?? throw new InvalidOperationException("Could not get DbContext");
        await context.Database.MigrateAsync();

        var connection = context.Database.GetDbConnection();

        if (strategy is not null)
        {
            await strategy.PrepopulateAsync(context, Fixture);
        }

        await connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" },
        });

        await connection.CloseAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        using IServiceScope scope = TestServer.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<TDbContext>() ?? throw new InvalidOperationException("Could not get DbContext");

        var connection = context.Database.GetDbConnection();

        await connection.OpenAsync();
        await _respawner.ResetAsync(context.Database.GetDbConnection());
        await connection.CloseAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            TestServer.Dispose();
        }
    }
}
;