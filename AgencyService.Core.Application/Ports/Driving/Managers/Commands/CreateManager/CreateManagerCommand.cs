using AgencyService.Core.Application.Ports.Driven.Repositories;
using AgencyService.Core.Domain.Entities;
using AgencyService.Core.Domain.Events;
using Mapster;
using MediatR;

namespace AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
public sealed record CreateManagerCommand(string Email, string FirstName, string LastName, string Group, int TravelAgencyId) : IRequest<Manager>;

public sealed class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, Manager>
{
    private readonly IManagerRepository _repository;
    private readonly IPublisher _publisher;

    public CreateManagerCommandHandler(IManagerRepository repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Manager> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await _repository.CreateAsync(request, cancellationToken);

        var managerEvent = result.Adapt<ManagerCreatedEvent>();
        await _publisher.Publish(managerEvent);

        return result;
    }
}
