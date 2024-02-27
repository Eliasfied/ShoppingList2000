using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions
{
    public static class WebApiServiceCollectionExtensions
    {
        public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var firebaseProjectId = configuration["Firebase:ProjectId"];
            services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
            {
                ApiKey = configuration["Firebase:ApiKey"],
                AuthDomain = $"{firebaseProjectId}.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                new EmailProvider(),
                new GoogleProvider()
                }
            }));

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var firebaseProjectId = configuration["Firebase:ProjectId"];
            var tokenBaseUrl = configuration["FireBase:TokenBaseUrl"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{tokenBaseUrl}/{firebaseProjectId}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"{tokenBaseUrl}/{firebaseProjectId}",
                        ValidateAudience = true,
                        ValidAudience = firebaseProjectId,
                        ValidateLifetime = true
                    };
                });

            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            return services;
        }
    }
}
