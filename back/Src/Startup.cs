using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Taskill.Configs;

namespace Taskill;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSettingsConfigs();
        services.AddServicesConfigs();

        services.AddRoutingConfigs();
        services.AddControllers();

        services.AddEfCoreConfigs();
        services.AddIdentityConfigs();

        services.AddAuthenticationConfigs();
        services.AddAuthorizationConfigs();

        services.AddSwaggerConfigs();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.SetupDatabase();

        app.UseRouting();

        app.UseDomainExceptions();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwaggerThings();

        app.UseEndpointsThings();
    }
}
