using AgencyService.Adapter.API.Models;
using AgencyService.Core.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyService.Adapter.API.Mapster;
public static class MapsterConfig
{
    public static IServiceCollection RegisterMapsterConfiguration(this IServiceCollection services)
    {
        //TypeAdapterConfig<TravelAgencyAccount, TravelAgencyDto>
        //    .NewConfig()
        //    .Map(dest => dest.UserId, src => src.UserId);

        return services;
    }
}
