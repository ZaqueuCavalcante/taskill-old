namespace Taskill.Configs;

public static class RoutingConfigs
{
    public static void AddRoutingConfigs(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
    }
}
