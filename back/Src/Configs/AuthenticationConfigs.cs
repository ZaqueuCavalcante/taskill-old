using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Taskill.Settings;

namespace Taskill.Configs;

public static class AuthenticationConfigs
{
    public const string BearerScheme = "Bearer";

    public static void AddAuthenticationConfigs(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var authSettings = serviceProvider.GetService<AuthSettings>()!;

        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authSettings.Issuer,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(authSettings.SecurityKey)
            ),

            ValidAlgorithms = new List<string> { "HS256" },

            ValidateAudience = true,
            ValidAudience = authSettings.Audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            NameClaimType = "sub",
        };

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = BearerScheme;
            options.DefaultAuthenticateScheme = BearerScheme;
        })
        .AddJwtBearer(BearerScheme, options =>
        {
            options.TokenValidationParameters = tokenValidationParameters;
        });
    }
}
