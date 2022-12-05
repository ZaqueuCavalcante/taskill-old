namespace Taskill.Configurations;

public static class AuthorizationConfigurations
{
    public const string TaskillerRole = "taskiller";

    public static void AddAuthorizationConfigurations(this IServiceCollection services)
    {
        services.AddAuthorization();
    }
}
