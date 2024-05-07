using AgencyService.Adapter.RabbitMQ.Models;
using AgencyService.Core.Application.Common.Interfaces;
using System.Text.Json;
using TravelAgency.SharedLibrary.Enums;
using TravelAgency.SharedLibrary.RabbitMQ.Interfaces;

namespace AgencyService.Adapter.RabbitMQ.Publishers;
public sealed class ManagerPublisher : IManagerPublisher
{
    private readonly IMessageBusPublisher _publisher;

    public ManagerPublisher(IMessageBusPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task PublishManagerCreated(int managerId, string email, string group, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var model = new ManagerCreatedPublishedDto { Event = EventTypes.ManagerCreated, Email = email, Group = group, ManagerId = managerId };
        var message = JsonSerializer.Serialize(model);
        await _publisher.Publish(message);
    }
}
