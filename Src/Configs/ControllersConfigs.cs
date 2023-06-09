using System.Text.Json.Serialization;

namespace Taskill.Configs;

public static class ControllersConfigs
{
    public static void AddControllersConfigs(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
        );
    }
}
