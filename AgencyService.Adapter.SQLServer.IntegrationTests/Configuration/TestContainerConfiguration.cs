using TestEnvironment.Docker;
using TestEnvironment.Docker.Containers.Mssql;

namespace AgencyService.Adapter.SQLServer.IntegrationTests.Configuration;
public class TestContainerConfiguration : IDisposable
{
    public IDockerEnvironment MsSqlDatabase { get; private set; }
    public IDockerEnvironment MockServer { get; private set; }

    public TestContainerConfiguration()
    {
        MsSqlDatabase = new DockerEnvironmentBuilder()
            .SetName("AgencyServiceDatabaseTest")
            .AddMssqlContainer(x => x with
            {
                Name = "AgencyServiceDB",
                Ports = new Dictionary<ushort, ushort>
                {
                    { 1433, 2100 }
                }
            }).Build();

        MockServer = new DockerEnvironmentBuilder().SetName("AgencyServiceMockServerTest")
            .AddContainer(x => x with
            {
                Name = "MockServer",
                ImageName = "mockserver/mockserver",
                Ports = new Dictionary<ushort, ushort>
                {
                    { 1080, 1080 }
                }
            }).Build();

        MsSqlDatabase.UpAsync().Wait();
        MockServer.UpAsync().Wait();
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
            MsSqlDatabase.DisposeAsync().AsTask().Wait();
            MockServer.DisposeAsync().AsTask().Wait();
        }
    }
}
