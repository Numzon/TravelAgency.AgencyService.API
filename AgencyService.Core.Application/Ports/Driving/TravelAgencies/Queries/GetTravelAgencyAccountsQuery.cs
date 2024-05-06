using AgencyService.Core.Domain.Entities;
using MediatR;

namespace AgencyService.Core.Application.Ports.Driving.TravelAgencies.Queries;
public sealed record GetTravelAgencyAccountsQuery(string SearchString) : IRequest<IEnumerable<TravelAgencyAccount>>;

public sealed class GetTravelAgencyAccountsQueryHandler : IRequestHandler<GetTravelAgencyAccountsQuery, IEnumerable<TravelAgencyAccount>>
{
    public Task<IEnumerable<TravelAgencyAccount>> Handle(GetTravelAgencyAccountsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
