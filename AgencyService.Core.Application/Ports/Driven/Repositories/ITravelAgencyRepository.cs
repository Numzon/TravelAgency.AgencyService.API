using AgencyService.Core.Domain.Entities;

namespace AgencyService.Core.Application.Ports.Driven;
public interface ITravelAgencyRepository
{
    Task<TravelAgencyAccount> CreateAsync(string userId, string agencyName, CancellationToken cancellationToken);
}
