using AgencyService.Core.Application.Ports.Driving.TravelAgencies.Commands;
using AgencyService.Core.Domain.Entities;

namespace AgencyService.Core.Application.Ports.Driven;
public interface ITravelAgencyRepository
{
    Task<TravelAgencyAccount> CreateAsync(CreateTravelAgencyCommand command, CancellationToken cancellationToken);
}
