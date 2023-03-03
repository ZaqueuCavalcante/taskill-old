namespace Taskill.Configs;

public static class CorsConfigs
{
    public static void AddCorsConfigs(this IServiceCollection services)
    {
        services.AddCors(options => options
            .AddDefaultPolicy(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            )
        );
    }
}
