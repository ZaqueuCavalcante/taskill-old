using Taskill.Configs;
using Taskill.Exceptions;
using Taskill.Services.Auth;
using Taskill.Settings;

namespace Taskill;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<AuthSettings>();
        services.AddSingleton<DatabaseSettings>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers();

        services.AddEfCoreConfigs();

        services.AddIdentityConfigs();

        services.AddAuthorizationConfigs();
        services.AddAuthenticationConfigs();

        services.AddSwaggerConfigs();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseMiddleware<DomainExceptionMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwaggerThings();

        app.UseEndpoints(builder => builder.MapControllers());
    }
}
