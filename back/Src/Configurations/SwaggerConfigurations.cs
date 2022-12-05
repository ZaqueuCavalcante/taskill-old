using System.Reflection;
using Microsoft.OpenApi.Models;
using static Taskill.Configurations.AuthenticationConfigurations;

namespace Taskill.Configurations;

public static class SwaggerConfigurations
{
    public static void AddSwaggerConfigurations(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Taskill",
                Version = "1.0",
                Description = "A API to a simple TODO app.",
                Contact = new OpenApiContact() { Name = "Zaqueu Cavalcante", Email = "zaqueudovale@gmail.com" },
                TermsOfService = new Uri("https://docs.github.com"),
                License = new OpenApiLicense() { Name = "License", Url = new Uri("https://opensource.org/licenses/MIT") }
            });

            options.AddSecurityDefinition(Scheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT authorization header using the bearer scheme. Past 'Bearer <jwt>' below.",
                Scheme = Scheme,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            options.DescribeAllParametersInCamelCase();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });
    }
}
