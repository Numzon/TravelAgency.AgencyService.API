using AgencyService.Adapter.API.Models;
using AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
using AgencyService.Core.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyService.Adapter.API.Mapster;
public static class MapsterConfig
{
    public static IServiceCollection RegisterApiMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Manager, ManagerDto>
            .NewConfig()
            .Map(dest => dest.FirstName, src => src.PersonalData.FirstName)
            .Map(dest => dest.LastName, src => src.PersonalData.LastName)
            .Map(dest => dest.Group, src => src.PersonalData.Group);        
        
        TypeAdapterConfig<CreateManagerCommand, Manager>
            .NewConfig()
            .Map(dest => dest.PersonalData.FirstName, src => src.FirstName)
            .Map(dest => dest.PersonalData.LastName, src => src.LastName)
            .Map(dest => dest.PersonalData.Group, src => src.Group);

        return services;
    }
}
