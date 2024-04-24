using AgencyService.Adapter.RabbitMQ.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.SharedLibrary.Models;
using TravelAgency.SharedLibrary.RabbitMQ;

namespace AgencyService.Adapter.RabbitMQ;
public static class ConfigureServices
{
    public static WebApplicationBuilder AddRabbitMQServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqSettingsDto>(builder.Configuration.GetRequiredSection("RabbitMQ"));

        builder.Services.AddSingleton(EventReceiverConfig.GetGlobalSettingsConfiguration());

        builder.Services.AddRabbitMqSubscriber();

        return builder;
    }
}
