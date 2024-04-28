using AgencyService.Adapter.SQLServer.Persistance;
using Microsoft.EntityFrameworkCore;

namespace AgencyService.IntegrationTests.Interfaces;
public interface IPrepopulateStrategy<TDbContext> 
    where TDbContext : DbContext
{
    Task PrepopulateAsync(TDbContext context, IFixture fixture);
        
}
