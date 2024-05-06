using AgencyService.Adapter.API;
using AgencyService.Adapter.RabbitMQ;
using AgencyService.Adapter.SQLServer;
using AgencyService.Core.Application;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddJsonFile("secrets/appsettings.Production.json", optional: true);
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