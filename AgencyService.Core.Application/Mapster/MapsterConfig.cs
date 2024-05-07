using AgencyService.Core.Application.Common.Models;
using AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
using AgencyService.Core.Domain.Entities;
using AgencyService.Core.Domain.Events;
using AgencyService.Core.Domain.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyService.Adapter.SQLServer.Mapster;
public static class MapsterConfig
{
    public static IServiceCollection RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateManagerCommand, Manager>
            .NewConfig()
            .Map(dest => dest.PersonalData, src => new PersonalData(src.FirstName, src.LastName, src.Group));

        TypeAdapterConfig<Manager, ManagerCreatedEvent>
            .NewConfig()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.ManagerId, src => src.Id)
            .Map(dest => dest.Group, src => src.PersonalData.Group);

        return services;
    }
}
