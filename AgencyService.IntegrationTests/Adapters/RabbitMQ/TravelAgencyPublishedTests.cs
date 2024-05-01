using AgencyService.Adapter.RabbitMQ.Models;
using AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;
using AgencyService.Adapter.SQLServer.Persistance;
using AgencyService.IntegrationTests.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TravelAgency.SharedLibrary.Enums;
using TravelAgency.SharedLibrary.Models;
using TravelAgency.SharedLibrary.RabbitMQ.Interfaces;

namespace AgencyService.IntegrationTests.Adapters.RabbitMQ;
public sealed class TravelAgencyPublishedTests : BaseIntegrationTest<AgencyServiceDbContext>
{
    [Fact]
    public async Task RetriveEvent_ValidEventTypeAndUserId_AddedEntityToDb()
    {
        using (TestServer = HostConfiguration.Build().Server)
        {
            await InitializeDatabaseAsync();

            using IServiceScope scope = TestServer.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<AgencyServiceDbContext>() ?? throw new InvalidOperationException("AgencyServiceDbContext cannot be null");
            var publisher = scope.ServiceProvider.GetService<IMessageBusPublisher>() ?? throw new InvalidOperationException("IMessageBusPublisher cannot be null");
            var dto = new TravelAgencyPublishedDto { Event = EventTypes.TravelAgencyUserCreated, UserId = Fixture.Create<string>(), AgencyName = Fixture.Create<string>() };

            //act
            await publisher.Publish(JsonSerializer.Serialize(dto));

            //assess
            var entity = await context.TravelAgencyAccount.FirstOrDefaultAsync(x => x.UserId == dto.UserId);
            entity.Should().NotBeNull();
            entity!.UserId.Should().Be(dto.UserId);

            //cleanup
            await ResetDatabaseAsync();
        }
    }

    [Fact]
    public async Task RetriveEvent_InvalidEventType_AddedEntityToDb()
    {
        using (TestServer = HostConfiguration.Build().Server)
        {
            await InitializeDatabaseAsync();

            using IServiceScope scope = TestServer.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<AgencyServiceDbContext>() ?? throw new InvalidOperationException("AgencyServiceDbContext cannot be null");
            var publisher = scope.ServiceProvider.GetService<IMessageBusPublisher>() ?? throw new InvalidOperationException("IMessageBusPublisher cannot be null");
            var dto = new TravelAgencyPublishedDto { Event = "Dummy_event", UserId = Fixture.Create<string>(), AgencyName = Fixture.Create<string>() };

            //act
            await publisher.Publish(JsonSerializer.Serialize(dto));

            //assess
            var entity = await context.TravelAgencyAccount.FirstOrDefaultAsync(x => x.UserId == dto.UserId);
            entity.Should().BeNull();

            //cleanup
            await ResetDatabaseAsync();
        }
    }
}
