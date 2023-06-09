using Taskill.Configs;
using System.Text.Json.Serialization;

namespace Taskill;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSettingsConfigs();
        services.AddServicesConfigs();

        services.AddRoutingConfigs();
        services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
        );

        services.AddEfCoreConfigs();
        services.AddIdentityConfigs();

        services.AddAuthenticationConfigs();
        services.AddAuthorizationConfigs();

        services.AddCorsConfigs();
        services.AddSwaggerConfigs();

        services.AddHealthChecks();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.UseCors();

        app.UseRouting();

        app.UseDomainExceptions();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwaggerThings();

        app.UseEndpointsThings();

        app.UseHealthChecks("/health");
    }
}
