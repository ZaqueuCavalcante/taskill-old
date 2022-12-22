using Taskill.Services;

namespace Taskill.Configs;

public static class ServicesConfigs
{
    public static void AddServicesConfigs(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITasksService, TasksService>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<ILabelsService, LabelsService>();
    }
}
