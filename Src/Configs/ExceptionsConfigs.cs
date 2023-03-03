using Taskill.Exceptions;

namespace Taskill.Configs;

public static class ExceptionsConfigs
{
    public static void UseDomainExceptions(this IApplicationBuilder app)
    {
        app.UseMiddleware<DomainExceptionMiddleware>();
    }
}
