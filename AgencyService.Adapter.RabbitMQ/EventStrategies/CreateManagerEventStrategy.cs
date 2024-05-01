using AgencyService.Adapter.RabbitMQ.Models;
using AgencyService.Core.Application.Common.Models;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TravelAgency.SharedLibrary.RabbitMQ.Interfaces;

namespace AgencyService.Adapter.RabbitMQ.EventStrategies;
public class CreateManagerEventStrategy : IEventStrategy
{
    public async Task ExecuteEvent(IServiceScope scope, string message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();   

        var manager  = JsonSerializer.Deserialize<ManagerPublishedDto>(message);

        if (manager is null)
        {
            throw new InvalidOperationException("Manager object cannot be null");
        }

        var repository = scope.ServiceProvider.GetRequiredService<IManagerRepository>();

        var createManagerDto = manager.Adapt<CreateManagerDto>();

        await repository.CreateAsync(createManagerDto, cancellationToken); 
    }
}
