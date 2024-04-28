using AgencyService.Adapter.API.Models;
using AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;
using AgencyService.Adapter.SQLServer.Persistance;
using AgencyService.Core.Application.Common.Models;
using AgencyService.Core.Application.Ports.Driving.TravelAgencies.Commands;
using AgencyService.IntegrationTests.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace AgencyService.IntegrationTests.Adapters.API;
public sealed class TravelAgencyControllerTests : BaseIntegrationTest<AgencyServiceDbContext>
{
    [Fact]
    public async Task CreateAsync_InvalidCommand_ThrowsValidationException()
    {
        var command = new CreateTravelAgencyCommand("");

        using (TestServer = HostConfiguration.Build().Server)
        {
            await InitializeDatabaseAsync();

            using HttpClient httpClient = TestServer.CreateClient();

            //act
            HttpResponseMessage httpResponse = await httpClient.PostAsync($"/api/travel-agencies", new StringContent(JsonConvert.SerializeObject(command, new StringEnumConverter()), Encoding.UTF8, MediaTypeNames.Application.Json));

            //assess
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var exceptionDto = JsonConvert.DeserializeObject<ValidationExceptionDto>(responseContent);

            exceptionDto.Should().NotBeNull();
            exceptionDto!.Message.Should().NotBeNullOrEmpty();   
            exceptionDto!.Errors.Should().NotBeNull().And.HaveCountGreaterThan(0);    

            //cleanup
            await ResetDatabaseAsync();
        }
    }

    [Fact]
    public async Task CreateAsync_ValidUserId_Created()
    {
        var command = new CreateTravelAgencyCommand(Fixture.Create<string>());

        using (TestServer = HostConfiguration.Build().Server)
        {
            //arrange
            await InitializeDatabaseAsync();

            using HttpClient httpClient = TestServer.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(command, new StringEnumConverter()), Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            HttpResponseMessage httpResponse = await httpClient.PostAsync($"/api/travel-agencies",content);

            //assess
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<TravelAgencyDto>(responseContent);

            dto.Should().NotBeNull();
            dto!.UserId.Should().Be(command.UserId);

            //cleanup
            await ResetDatabaseAsync();
        }
    }
}
