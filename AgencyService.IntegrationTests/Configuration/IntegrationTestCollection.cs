using AgencyService.Adapter.SQLServer.IntegrationTests.Enums;

namespace AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;

[CollectionDefinition(CollectionDefinitions.IntergrationTestCollection)]
public class IntegrationTestCollection : ICollectionFixture<TestContainerConfiguration>
{
    //configuration, no code here, class will not be ever created
}
