using AgencyService.Adapter.RabbitMQ.EventStrategies;
using AgencyService.Adapter.RabbitMQ.Models;
using AgencyService.Core.Application.Ports.Driven;
using AgencyService.Core.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace AgencyService.Adapter.RabbitMQ.Tests.EventStrategies;
public sealed class CreateTravelAgencyEventStrategyTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IServiceScope> _serviceScope;
    private readonly Mock<IServiceProvider> _serviceProvider;
    private readonly Mock<ITravelAgencyRepository> _repository;

    public CreateTravelAgencyEventStrategyTests()
    {
        _fixture = new Fixture();
        _serviceScope = new Mock<IServiceScope>();
        _serviceProvider = new Mock<IServiceProvider>();
        _repository = new Mock<ITravelAgencyRepository>();

        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList().ForEach(behavior => _fixture.Behaviors.Remove(behavior));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _repository.Setup(x => x.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<TravelAgencyAccount>());
        _serviceScope.Setup(x => x.ServiceProvider).Returns(_serviceProvider.Object);
        _serviceProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(_repository.Object);
    }

    [Fact]
    public async Task ExecuteEvent_MessageStringIsNull_ThrowsArgumentNullException()
    {
        string message = null!;
        var strategy = new CreateTravelAgencyEventStrategy();

        await strategy.Invoking(x => x.ExecuteEvent(_serviceScope.Object, message, default)).Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ExecuteEvent_MessageFormatIsIncorrect_ExecuteSuccessfully()
    {
        var strategy = new CreateTravelAgencyEventStrategy();
        var publishedDto = _fixture.Create<TravelAgencyPublishedDto>();
        var message = JsonSerializer.Serialize(publishedDto);

        await strategy.Invoking(x => x.ExecuteEvent(_serviceScope.Object, message, default)).Should().NotThrowAsync();
    }
}
