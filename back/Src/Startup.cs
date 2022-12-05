using Taskill.Configurations;
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

        services.AddEfCoreConfigurations();

        services.AddIdentityConfigurations();

        services.AddAuthorizationConfigurations();
        services.AddAuthenticationConfigurations();

        services.AddSwaggerConfigurations();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Taskill 1.0");
            options.DefaultModelsExpandDepth(-1);
        });

        app.UseEndpoints(builder => builder.MapControllers());
    }
}
