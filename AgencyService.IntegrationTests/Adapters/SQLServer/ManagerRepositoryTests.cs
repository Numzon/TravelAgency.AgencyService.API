using AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;
using AgencyService.Adapter.SQLServer.Persistance;
using AgencyService.Core.Application.Common.Models;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
using AgencyService.Core.Domain.Entities;
using AgencyService.IntegrationTests.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyService.IntegrationTests.Adapters.SQLServer;
public sealed class ManagerRepositoryTests : BaseIntegrationTest<AgencyServiceDbContext>
{
    public ManagerRepositoryTests() : base()
    {
        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task CreateAsync_ValidCreateManagerDto_SuccessfullyAddedNewEntity()
    {
        using (TestServer = HostConfiguration.Build().Server)
        {
            //arrange
            await InitializeDatabaseAsync();

            using IServiceScope scope = TestServer.Services.CreateScope();
            var repository = scope.ServiceProvider.GetService<IManagerRepository>() ?? throw new InvalidOperationException("IManagerRepository cannot be null");
            var db = scope.ServiceProvider.GetService<AgencyServiceDbContext>() ?? throw new InvalidOperationException("AgencyServiceDbContext cannot be null");
            var travelAgency = Fixture.Build<TravelAgencyAccount>().With(x => x.Id, 0)
                .Without(x => x.Comments)
                .Without(x => x.Managers)
                .Without(x => x.Reports)
                .Without(x => x.BankAccountDataId)
                .Without(x => x.BankAccountData)
                .Create();

            await db.TravelAgencyAccount.AddAsync(travelAgency);
            await db.SaveChangesAsync();

            var managerDto = Fixture.Build<CreateManagerCommand>().With(x => x.TravelAgencyId, travelAgency.Id).Create();

            //act
            var entity = await repository.CreateAsync(managerDto, default);

            //assess
            var fetched = await db.Manager.FirstOrDefaultAsync(x => x.Id == entity.Id);
            entity.Should().NotBeNull();
            entity.Id.Should().NotBe(0);
            fetched.Should().NotBeNull();
            fetched!.Id.Should().Be(entity.Id);
            fetched!.TravelAgencyId.Should().Be(managerDto.TravelAgencyId);

            //cleanup
            await ResetDatabaseAsync();
        }
    }
}
