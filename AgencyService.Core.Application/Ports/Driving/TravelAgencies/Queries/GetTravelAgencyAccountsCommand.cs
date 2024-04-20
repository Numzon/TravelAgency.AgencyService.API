using AgencyService.Core.Domain.Entities;
using MediatR;

namespace AgencyService.Core.Application.Ports.Driving.TravelAgencies.Queries;
public sealed record GetTravelAgencyAccountsCommand(string SearchString) : IRequest<IEnumerable<TravelAgencyAccount>>;

public sealed class GetTravelAgencyAccountsCommandHandler : IRequestHandler<GetTravelAgencyAccountsCommand, IEnumerable<TravelAgencyAccount>>
{
    public Task<IEnumerable<TravelAgencyAccount>> Handle(GetTravelAgencyAccountsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
