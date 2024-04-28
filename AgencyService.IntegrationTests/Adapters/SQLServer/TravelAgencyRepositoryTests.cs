using AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;
using AgencyService.Adapter.SQLServer.Persistance;
using AgencyService.Core.Application.Ports.Driven;
using AgencyService.IntegrationTests.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace AgencyService.IntegrationTests.Adapters.SQLServer;
public sealed class TravelAgencyRepositoryTests : BaseIntegrationTest<AgencyServiceDbContext>
{
    [Fact]
    public async Task CreateAsync_ValidUserId_SuccessfullyAddedNewEntity()
    {
        using (TestServer = HostConfiguration.Build().Server)
        {
            //arrange
            await InitializeDatabaseAsync();

            using IServiceScope scope = TestServer.Services.CreateScope();
            var repository = scope.ServiceProvider.GetService<ITravelAgencyRepository>() ?? throw new InvalidOperationException("ITravelAgencyRepository cannot be null");
            var db = scope.ServiceProvider.GetService<AgencyServiceDbContext>() ?? throw new InvalidOperationException("AgencyServiceDbContext cannot be null");
            var userId = Fixture.Create<string>();

            //act
            var entity = await repository.CreateAsync(userId, default);

            //assess
            var fetched = await db.TravelAgencyAccount.FirstOrDefaultAsync(x => x.UserId == userId);
            entity.Should().NotBeNull();
            entity.Id.Should().NotBe(0);
            entity.UserId.Should().Be(userId);
            fetched.Should().NotBeNull();
            fetched!.Id.Should().Be(entity.Id);
            fetched!.UserId.Should().Be(userId);

            //cleanup
            await ResetDatabaseAsync();
        }
    }

    [Fact]
    public async Task CreateAsync_UserIdIsNull_ThrowsException()
    {
        using (TestServer = HostConfiguration.Build().Server)
        {
            //arrange
            await InitializeDatabaseAsync();

            using IServiceScope scope = TestServer.Services.CreateScope();
            var repository = scope.ServiceProvider.GetService<ITravelAgencyRepository>() ?? throw new InvalidOperationException("ITravelAgencyRepository cannot be null");
            string userId = null!;

            //act assess
            await repository.Invoking(x => x.CreateAsync(userId, default)).Should().ThrowAsync<DbUpdateException>();

            //cleanup
            await ResetDatabaseAsync();
        }
    }
}
