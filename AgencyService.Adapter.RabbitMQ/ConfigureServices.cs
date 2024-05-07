using AgencyService.Adapter.RabbitMQ.Configs;
using AgencyService.Adapter.RabbitMQ.Mapster;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using AgencyService.Core.Application.Ports.Driven;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TravelAgency.SharedLibrary.Models;
using TravelAgency.SharedLibrary.RabbitMQ;
using AgencyService.Core.Application.Common.Interfaces;
using AgencyService.Adapter.RabbitMQ.Publishers;

namespace AgencyService.Adapter.RabbitMQ;
public static class ConfigureServices
{
    public static WebApplicationBuilder AddRabbitMQServices(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterMapsterConfiguration();

        builder.Services.Configure<RabbitMqSettingsDto>(builder.Configuration.GetRequiredSection("RabbitMQ"));

        builder.Services.AddSingleton(EventReceiverConfig.GetGlobalSettingsConfiguration());

        try
        {
            var settings = builder.Configuration.GetRequiredSection("RabbitMQ").Get<RabbitMqSettingsDto>();
            builder.Services.AddRabbitMqConfiguration(settings!);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
        }

        builder.Services.RegisterPublishers();

        return builder;
    }

    private static IServiceCollection RegisterPublishers(this IServiceCollection services)
    {
        services.AddScoped<IManagerPublisher, ManagerPublisher>();

        return services;
    }
}
