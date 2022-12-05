using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Taskill.Settings;

namespace Taskill.Configurations;

public static class AuthenticationConfigurations
{
    public const string Scheme = "Bearer";

    public static void AddAuthenticationConfigurations(this IServiceCollection services)
    {
        var authSettings = services.BuildServiceProvider().GetService<AuthSettings>()!;

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

            ValidateAudience = true,
            ValidAudience = authSettings.Audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            NameClaimType = "sub",
            RoleClaimType = "role",
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = Scheme;
            options.DefaultScheme = Scheme;
            options.DefaultChallengeScheme = Scheme;
        })
        .AddJwtBearer(Scheme, options =>
        {
            options.SaveToken = true;

            options.TokenValidationParameters = tokenValidationParameters;
        });
    }
}
