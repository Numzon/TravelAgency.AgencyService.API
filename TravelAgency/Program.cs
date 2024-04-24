using AgencyService.Adapter.API;
using AgencyService.Adapter.RabbitMQ;
using AgencyService.Adapter.SQLServer;
using AgencyService.Core.Application;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiServices(typeof(Program).Assembly);
builder.AddApplicationServices();
builder.AddSqlServerServices();
builder.AddRabbitMQServices();

var app = builder.Build();

app.AddApiMiddlewares();
await app.InitializeDatabase();

app.Run();
