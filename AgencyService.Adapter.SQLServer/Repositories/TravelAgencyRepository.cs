using AgencyService.Adapter.SQLServer.Interfaces;
using AgencyService.Core.Application.Ports.Driven;
using AgencyService.Core.Application.Ports.Driving.TravelAgencies.Commands;
using AgencyService.Core.Domain.Entities;

namespace AgencyService.Adapter.SQLServer.Repositories;
public class TravelAgencyRepository : ITravelAgencyRepository
{
    private readonly IAgencyServiceDbContext _context;

    public TravelAgencyRepository(IAgencyServiceDbContext context)
    {
        _context = context;
    }

    public async Task<TravelAgencyAccount> CreateAsync(CreateTravelAgencyCommand command, CancellationToken cancellationToken)
    {
        var agency = new TravelAgencyAccount { UserId = command.UserId };

        await _context.TravelAgencyAccount.AddAsync(agency, cancellationToken);
        await _context.SaveChangesAsync();

        return agency;
    }

    //public async Task<TravelAgencyAccount> GetByFilterAsync(CancellationToken cancellationToken)
    //{

    //}
}
