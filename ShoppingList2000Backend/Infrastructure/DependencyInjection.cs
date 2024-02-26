using Application.Interfaces;
using Application.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) {

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("firebase.json")
        });

        services.AddSingleton<IAuthenticationService, FireBaseAuthenticationService>();

     

        return services;
        }
    }

