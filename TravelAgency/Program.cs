using AgencyService.Adapter.API;
using AgencyService.Adapter.RabbitMQ;
using AgencyService.Adapter.SQLServer;
using AgencyService.Core.Application;
using TravelAgency.SharedLibrary.Vault;
using TravelAgency.SharedLibrary.Vault.Consts;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    var vaultBuilder = new VaultFacadeBuilder();

    var vaultFacade = vaultBuilder
                        .SetToken(Environment.GetEnvironmentVariable(VaultEnvironmentVariables.Token))
                        .SetPort(Environment.GetEnvironmentVariable(VaultEnvironmentVariables.Port))
                        .SetHost(Environment.GetEnvironmentVariable(VaultEnvironmentVariables.Host))
                        .SetSSL(false)
                        .Build();

    var rabbitMq = await vaultFacade.ReadRabbitMqSecretAsync();
    var connectionString = await vaultFacade.ReadAgencyServiceConnectionStringSecretAsync();
    var cognito = await vaultFacade.ReadCognitoSecretAsync();

    builder.Configuration.AddInMemoryCollection(rabbitMq);
    builder.Configuration.AddInMemoryCollection(connectionString);
    builder.Configuration.AddInMemoryCollection(cognito);
}

builder.AddApiServices(typeof(Program).Assembly);
builder.AddApplicationServices();
builder.AddSqlServerServices();
builder.AddRabbitMQServices();

var app = builder.Build();

app.AddApiMiddlewares();
await app.InitializeDatabase();

app.Run();

#pragma warning disable S1118 // Utility classes should not have public constructors
public partial class Program { }
#pragma warning restore S1118 // Utility classes should not have public constructors