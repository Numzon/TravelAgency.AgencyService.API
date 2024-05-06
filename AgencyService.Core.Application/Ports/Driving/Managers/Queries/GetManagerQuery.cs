using AgencyService.Core.Application.Common.Exceptions;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using AgencyService.Core.Domain.Entities;
using MediatR;

namespace AgencyService.Core.Application.Ports.Driving.Managers.Queries;
public sealed record GetManagerQuery(int Id) : IRequest<Manager>;

public sealed class GetManagerQueryHandler : IRequestHandler<GetManagerQuery, Manager>
{
    private readonly IManagerRepository _repository;

    public GetManagerQueryHandler(IManagerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Manager> Handle(GetManagerQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var manager = await _repository.GetManagerAsync(request.Id, cancellationToken);

        if (manager is null)
        {
            throw new NotFoundException($"Manager with id {request.Id} not found");
        }

        return manager;
    }
}
