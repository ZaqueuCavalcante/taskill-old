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

        services.AddCorsConfigs();
        services.AddSwaggerConfigs();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.SetupDatabase();

        app.UseCors();

        app.UseRouting();

        app.UseDomainExceptions();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwaggerThings();

        app.UseEndpointsThings();
    }
}