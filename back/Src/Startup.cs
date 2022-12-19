using Taskill.Configs;
using Taskill.Exceptions;
using Taskill.Services.Auth;
using Taskill.Services.Labels;
using Taskill.Services.Projects;
using Taskill.Services.Tasks;
using Taskill.Settings;

namespace Taskill;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<AuthSettings>();
        services.AddSingleton<DatabaseSettings>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITasksService, TasksService>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<ILabelsService, LabelsService>();

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
