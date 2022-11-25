using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Settings;

namespace Taskill.Configurations;

public static class EfCoreConfigurations
{
    public static void AddEfCoreConfigurations(this IServiceCollection services)
    {
        var databaseSettings = services.BuildServiceProvider().GetService<DatabaseSettings>()!;

        services.AddDbContext<TaskillDbContext>(options =>
        {
            options.UseNpgsql(databaseSettings.ConnectionString);
            options.UseSnakeCaseNamingConvention();
        });
    }
}
