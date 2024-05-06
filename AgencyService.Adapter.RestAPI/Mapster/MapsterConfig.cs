using AgencyService.Adapter.API.Models;
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

        return services;
    }
}
