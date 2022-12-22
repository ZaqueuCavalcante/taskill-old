using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Settings;

namespace Taskill.Configs;

public static class EfCoreConfigs
{
    public static void AddEfCoreConfigs(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var databaseSettings = services.BuildServiceProvider().GetService<DatabaseSettings>()!;

        services.AddDbContext<TaskillDbContext>(options =>
        {
            options.UseNpgsql(databaseSettings.ConnectionString);
            options.UseSnakeCaseNamingConvention();
        });
    }

    public static void SetupDatabase(this IApplicationBuilder app)
    {
        if (Env.IsDevelopment())
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaskillDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
