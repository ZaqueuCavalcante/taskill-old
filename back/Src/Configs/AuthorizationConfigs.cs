namespace Taskill.Configs;

public static class AuthorizationConfigs
{
    public const string PremiumClaim = "premium";
    public const string PremiumPolicy = nameof(PremiumPolicy);

    public static void AddAuthorizationConfigs(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
            options.AddPolicy(PremiumPolicy, pb => pb.RequireClaim(PremiumClaim, "true"))
        );
    }
}
