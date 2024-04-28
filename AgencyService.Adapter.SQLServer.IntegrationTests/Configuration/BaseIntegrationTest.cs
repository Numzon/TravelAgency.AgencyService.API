using AgencyService.Adapter.SQLServer.IntegrationTests.Enums;
using Microsoft.AspNetCore.TestHost;
using MockServerClientNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AgencyService.Adapter.SQLServer.Persistance;
using AgencyService.Adapter.SQLServer.IntegrationTests.Fixtures;

namespace AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;

[Collection(CollectionDefinitions.IntergrationTestCollection)]
public abstract class BaseIntegrationTest
{
    protected TestServer TestServer { get; set; }
    protected IFixture Fixture { get; private set; }

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

    protected async Task ResetAndInitDatabase(Dataset dataset = Dataset.Empty)
    {
        IServiceProvider serviceProvider = TestServer.Services;

        using IServiceScope scope = serviceProvider.CreateScope();
        using AgencyServiceDbContext dbContext = scope.ServiceProvider.GetService<AgencyServiceDbContext>() ?? throw new InvalidOperationException("Could not get AgencyServiceDbContext");

        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM TravelAgencyAccount");

        switch (dataset)
        {
            case Dataset.Fetch:
                await TravelAgencyAccountFixtures.PopulateDataTestForFetching(dbContext, Fixture);
                break;
            default:
                break;
        }
    }
}
