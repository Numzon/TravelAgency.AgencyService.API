using AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;

namespace AgencyService.IntegrationTests.Adapters;
public sealed class TravelAgencyControllerTests : BaseIntegrationTest
{
    [Fact]
    public async Task CreateAsync_InvalidCommand_ThrowsValidationException()
    {
       // using (TestServer = HOstCOnfi)
    }
}
