using AgencyService.Adapter.RabbitMQ.EventStrategies;
using TravelAgency.SharedLibrary.Enums;
using TravelAgency.SharedLibrary.RabbitMQ;

namespace AgencyService.Adapter.RabbitMQ.Configs;
public static class EventReceiverConfig
{
    public static TypeEventStrategyConfig GetGlobalSettingsConfiguration()
    {
        var config = TypeEventStrategyConfig.GlobalSetting;

        config.NewConfig<CreateTravelAgencyEventStrategy>(EventTypes.TravelAgencyUserCreated);
        config.NewConfig<UserForManagerCreatedEventStrategy>(EventTypes.UserForManagerCreated);

        return config;
    }
}
