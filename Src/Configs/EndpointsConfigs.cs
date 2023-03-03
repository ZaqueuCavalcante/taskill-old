namespace Taskill.Configs;

public static class EndpointsConfigs
{
    public static void UseEndpointsThings(this IApplicationBuilder app)
    {
        app.UseEndpoints(builder => builder.MapControllers());
    }
}
