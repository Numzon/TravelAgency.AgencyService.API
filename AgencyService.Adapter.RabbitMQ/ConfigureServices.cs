using AgencyService.Adapter.RabbitMQ.Configs;
using AgencyService.Adapter.RabbitMQ.Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TravelAgency.SharedLibrary.Models;
using TravelAgency.SharedLibrary.RabbitMQ;

namespace AgencyService.Adapter.RabbitMQ;
public static class ConfigureServices
{
    public static WebApplicationBuilder AddRabbitMQServices(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterMapsterConfiguration();

        builder.Services.Configure<RabbitMqSettingsDto>(builder.Configuration.GetRequiredSection("RabbitMQ"));

        builder.Services.AddRabbitMqPublisher();

        builder.Services.AddSingleton(EventReceiverConfig.GetGlobalSettingsConfiguration());

        try
        {
            var settings = builder.Configuration.GetRequiredSection("RabbitMQ").Get<RabbitMqSettingsDto>();
            builder.Services.AddRabbitMqSubscriber(settings!);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
        }

        return builder;
    }
}
