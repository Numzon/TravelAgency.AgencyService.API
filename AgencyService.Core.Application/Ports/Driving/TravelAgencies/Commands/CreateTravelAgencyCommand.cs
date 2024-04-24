using AgencyService.Core.Application.Ports.Driven;
using AgencyService.Core.Domain.Entities;
using MediatR;

namespace AgencyService.Core.Application.Ports.Driving.TravelAgencies.Commands;
public sealed record CreateTravelAgencyCommand(string UserId) : IRequest<TravelAgencyAccount>;

public sealed record CreateTravelAgencyCommandHandler : IRequestHandler<CreateTravelAgencyCommand, TravelAgencyAccount>
{
    private readonly ITravelAgencyRepository _repository;

    public CreateTravelAgencyCommandHandler(ITravelAgencyRepository repository)
    {
        _repository = repository;
    }

    public async Task<TravelAgencyAccount> Handle(CreateTravelAgencyCommand request, CancellationToken cancellationToken)
    {
        var agency = await _repository.CreateAsync(request.UserId, cancellationToken);

        return agency;
    }
}
