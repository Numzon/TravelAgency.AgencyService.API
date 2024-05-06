using AgencyService.Core.Application.Common.Models;
using AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
using AgencyService.Core.Domain.Entities;
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

        return services;
    }
}
