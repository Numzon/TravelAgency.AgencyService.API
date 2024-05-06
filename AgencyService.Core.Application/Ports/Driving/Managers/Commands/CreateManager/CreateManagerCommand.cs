using AgencyService.Core.Application.Common.Interfaces;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using AgencyService.Core.Domain.Entities;
using MediatR;

namespace AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
public sealed record CreateManagerCommand(string Email, string FirstName, string LastName, string Group, int TravelAgencyId) : IRequest<Manager>;

public sealed class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, Manager>
{
    private readonly IManagerRepository _repository;
    private readonly IManagerPublisher _publisher;

    public CreateManagerCommandHandler(IManagerRepository repository, IManagerPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Manager> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await _repository.CreateAsync(request, cancellationToken);

        await _publisher.PublishManagerCreated(request.Email, request.Group, cancellationToken);

        return result;
    }
}
