using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;

namespace AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;
public class TestContainerConfiguration : IDisposable
{
    public IContainer MsSqlDatabase { get; private set; }
    public IContainer MockServer { get; private set; }
    public IContainer RabbitMqContainer { get; private set; }

    public TestContainerConfiguration()
    {
        MsSqlDatabase = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithName("SQLServerTests")
            .WithPortBinding(2100, 1433)
            .WithPassword("test.1234")
            .Build();

        MockServer = new ContainerBuilder()
            .WithImage("mockserver/mockserver")
            .WithName("MockServer")
            .WithPortBinding(1090, 1080)
            .Build();

        RabbitMqContainer = new RabbitMqBuilder()
            .WithImage("rabbitmq:3-management")
            .WithName("RabbitMqTests")
            .WithPortBinding(15600, 15672)
            .WithPortBinding(5600, 5672)
            .WithUsername(null)
            .WithPassword(null)
            .Build();

        MsSqlDatabase.StartAsync().Wait();
        MockServer.StartAsync().Wait();
        RabbitMqContainer.StartAsync().Wait();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            MsSqlDatabase.StopAsync().Wait();
            MockServer.StopAsync().Wait();
            RabbitMqContainer.StopAsync().Wait();
        }
    }
}
