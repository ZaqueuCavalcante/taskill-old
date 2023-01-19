using Microsoft.AspNetCore.Identity;
using Taskill.Database;
using Taskill.Domain;

namespace Taskill.Configs;

public static class IdentityConfigs
{
    public static void AddIdentityConfigs(this IServiceCollection services)
    {
        services.AddIdentity<Taskiller, IdentityRole<uint>>()
            .AddEntityFrameworkStores<TaskillDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(1)
        );

        services.Configure<IdentityOptions>(options =>
            options.User.RequireUniqueEmail = true
        );

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;  // The minimum length.
            options.Password.RequireDigit = true;  // Requires a number between 0-9.
            options.Password.RequireLowercase = true;  // Requires a lowercase character.
            options.Password.RequireUppercase = true;  // Requires an uppercase character.
            options.Password.RequireNonAlphanumeric = true;  // Requires a non-alphanumeric character (@, %, #, !, &, $, ...).
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.AllowedForNewUsers = true;  // Determines if a new user can be locked out.
            options.Lockout.MaxFailedAccessAttempts = 10;  // The number of failed access attempts until a user is locked out, if lockout is enabled.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(24);  // The amount of time a user is locked out when a lockout occurs.
        });
    }
}
