using AgencyService.Adapter.API.Mapster;
using AgencyService.Adapter.API.Middlewares;
using Amazon;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection;
using TravelAgency.SharedLibrary.AWS;
using TravelAgency.SharedLibrary.Models;
using TravelAgency.SharedLibrary.Swagger;

namespace AgencyService.Adapter.API;
public static class ConfigureServices
{
    public static WebApplicationBuilder AddApiServices(this WebApplicationBuilder builder, Assembly assembly)
    {
        builder.Configuration.AddAndConfigureSecretManager(builder.Environment, RegionEndpoint.EUNorth1);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAndConfigureSwagger(assembly.GetName().Name!);

        try
        {
            var cognitoConfiguration = builder.Configuration.GetRequiredSection("AWS:Cognito").Get<AwsCognitoSettingsDto>();
            builder.Services.AddAuthenticationAndJwtConfiguration(cognitoConfiguration!);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
        }

        builder.Services.AddAuthorizationWithPolicies();
        builder.Services.RegisterApiMapsterConfiguration();

        return builder;
    }

    public static WebApplication AddApiMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}