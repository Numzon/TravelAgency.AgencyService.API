using AgencyService.Adapter.RabbitMQ.Models;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TravelAgency.SharedLibrary.RabbitMQ.Interfaces;

namespace AgencyService.Adapter.RabbitMQ.EventStrategies;
public class UserForManagerCreatedEventStrategy : IEventStrategy
{
    public async Task ExecuteEvent(IServiceScope scope, string message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var publishedDto = JsonSerializer.Deserialize<UserForManagerCreatedPublishedDto>(message);

        if (publishedDto is null)
        {
            throw new InvalidOperationException("UserForManager object cannot be null");
        }

        var repository = scope.ServiceProvider.GetRequiredService<IManagerRepository>();

        await repository.UpdateUserIdAsync(publishedDto.ManagerId, publishedDto.UserId, cancellationToken);
    }
}
