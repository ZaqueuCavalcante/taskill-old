using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Taskill.Settings;

namespace Taskill.Configs;

public static class AuthenticationConfigs
{
    public const string BearerScheme = "Bearer";
    public const string GoogleScheme = "Google";

    public static void AddAuthenticationConfigs(this IServiceCollection services)
    {
        var authSettings = services.BuildServiceProvider().GetService<AuthSettings>()!;
        var googleSettings = services.BuildServiceProvider().GetService<GoogleSettings>()!;

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
            options.DefaultAuthenticateScheme = BearerScheme;
            options.DefaultScheme = BearerScheme;
            options.DefaultChallengeScheme = BearerScheme;
        })
        .AddJwtBearer(BearerScheme, options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = tokenValidationParameters;
        })
        .AddGoogle(GoogleScheme, options =>
        {
            options.ClientId = googleSettings.ClientId;
            options.ClientSecret = googleSettings.ClientSecret;
            options.SignInScheme = IdentityConstants.ExternalScheme;
        });
    }
}
