using AgencyService.Adapter.SQLServer.Persistance;
using AgencyService.Core.Domain.Common;
using AgencyService.Core.Domain.Entities;

namespace AgencyService.Adapter.SQLServer.IntegrationTests.Fixtures;
public static class TravelAgencyAccountFixtures
{
    public static async Task PopulateDataTestForFetching(AgencyServiceDbContext context, IFixture fixture)
    {
        var entities = fixture.Build<TravelAgencyAccount>()
                                            .With(x => x.DomainEvents, new List<BaseEvent>())
                                            .CreateMany(10);

        context.AddRange(entities);
        await context.SaveChangesAsync();
    }
}
