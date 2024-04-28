using AgencyService.Adapter.SQLServer.Persistance;
using AgencyService.Core.Domain.Common;
using AgencyService.Core.Domain.Entities;
using AgencyService.IntegrationTests.Interfaces;

namespace AgencyService.Adapter.SQLServer.IntegrationTests.Fixtures;
public class TravelAgencyAccountPrepopulateStrategy : IPrepopulateStrategy<AgencyServiceDbContext>
{
    public async Task PrepopulateAsync(AgencyServiceDbContext context, IFixture fixture)
    {
        var entities = fixture.Build<TravelAgencyAccount>()
                                            .With(x => x.DomainEvents, new List<BaseEvent>())
                                            .CreateMany(10);

        context.AddRange(entities);
        await context.SaveChangesAsync();
    }
}
