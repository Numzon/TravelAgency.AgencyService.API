using AgencyService.Adapter.RabbitMQ.Models;
using AgencyService.Core.Application.Ports.Driven;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TravelAgency.SharedLibrary.RabbitMQ.Interfaces;

namespace AgencyService.Adapter.RabbitMQ.EventStrategies;
public sealed class CreateTravelAgencyEventStrategy : IEventStrategy
{
    public async Task ExecuteEvent(IServiceScope scope, string message)
    {
        var travelAgencyData = JsonSerializer.Deserialize<TravelAgencyPublishedDto>(message);

        if (travelAgencyData is null)
        {
            throw new InvalidOperationException("Cannot convert to travel agency object");
        }

        var repository = scope.ServiceProvider.GetRequiredService<ITravelAgencyRepository>();

        //TO DO
        await repository.CreateAsync(travelAgencyData.UserId, travelAgencyData.AgencyName, default);
    }
}
