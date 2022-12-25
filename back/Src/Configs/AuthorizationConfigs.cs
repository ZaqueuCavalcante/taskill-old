namespace Taskill.Configs;

public static class AuthorizationConfigs
{
    public const string TaskillerRole = "taskiller";
    public const string PremiumRole = "premium";

    public static void AddAuthorizationConfigs(this IServiceCollection services)
    {
        services.AddAuthorization();
    }
}
