using Microsoft.Extensions.DependencyInjection;

namespace AgencyService.Adapter.RabbitMQ.Mapster;
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
