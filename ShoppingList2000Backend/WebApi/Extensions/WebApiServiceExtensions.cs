using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Extensions
{
    public static class WebApiServiceCollectionExtensions
    {
        public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var firebaseProjectName = configuration["Firebase:ProjectName"];
            services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
            {
                ApiKey = configuration["Firebase:ApiKey"],
                AuthDomain = $"{firebaseProjectName}.firebaseapp.com",
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
            var firebaseProjectName = configuration["Firebase:ProjectName"];
            var tokenBaseUrl = configuration["FireBase:TokenBaseUrl"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{tokenBaseUrl}/{firebaseProjectName}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"{tokenBaseUrl}/{firebaseProjectName}",
                        ValidateAudience = true,
                        ValidAudience = firebaseProjectName,
                        ValidateLifetime = true
                    };
                });

            return services;
        }
    }
}
