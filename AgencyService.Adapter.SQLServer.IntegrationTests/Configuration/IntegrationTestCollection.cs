using AgencyService.Adapter.SQLServer.IntegrationTests.Enums;

namespace AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;

[CollectionDefinition(CollectionDefinitions.IntergrationTestCollection)]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestCollection>
{
    //configuration, no code here, class will not be ever created
}
