using AgencyService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyService.Adapter.SQLServer.Interfaces;
public interface IAgencyServiceDbContext
{
    DbSet<TravelAgencyAccount> TravelAgencyAccount { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
