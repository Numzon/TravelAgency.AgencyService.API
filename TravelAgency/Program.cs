using AgencyService.Adapter.API;
using AgencyService.Adapter.SQLServer;
using AgencyService.Core.Application;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiServices(typeof(Program).Assembly);
builder.AddApplicationServices();
builder.AddSqlServerServices();

var app = builder.Build();

app.AddApiMiddlewares();

app.Run();
