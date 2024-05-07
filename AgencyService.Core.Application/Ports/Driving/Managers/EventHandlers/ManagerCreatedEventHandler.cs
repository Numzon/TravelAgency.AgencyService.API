using AgencyService.Core.Application.Common.Interfaces;
using AgencyService.Core.Domain.Events;
using MediatR;

namespace AgencyService.Core.Application.Ports.Driving.Managers.EventHandlers;
public sealed class ManagerCreatedEventHandler : INotificationHandler<ManagerCreatedEvent>
{
    private readonly IManagerPublisher _publisher;

    public ManagerCreatedEventHandler(IManagerPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(ManagerCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _publisher.PublishManagerCreated(notification.ManagerId, notification.Email, notification.Group, cancellationToken);
    }
}
