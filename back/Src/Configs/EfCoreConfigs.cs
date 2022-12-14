using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Settings;

namespace Taskill.Configs;

public static class EfCoreConfigs
{
    public static void AddEfCoreConfigs(this IServiceCollection services)
    {
        var databaseSettings = services.BuildServiceProvider().GetService<DatabaseSettings>()!;

        services.AddDbContext<TaskillDbContext>(options =>
        {
            options.UseNpgsql(databaseSettings.ConnectionString);
            options.UseSnakeCaseNamingConvention();
        });
    }
}
