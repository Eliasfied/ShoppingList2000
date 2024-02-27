using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using AutoMapper;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Net.Client;
using Infrastructure.Mappings;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

        var assembly = typeof(DependencyInjection).Assembly;

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("firebase.json")
        });

        services.AddAutoMapper(assembly, typeof(ShoppingListProfileInfrastructure).Assembly);

        services.AddSingleton<IAuthenticationService, FireBaseAuthenticationService>();

        var projectId = configuration["Firebase:ProjectId"];
        var filepath = configuration["FireBase:ServiceAccountPath"];
        string jsonContent = System.IO.File.ReadAllText(filepath);


        FirestoreDb firestoreDb = new FirestoreDbBuilder
        {
            ProjectId = projectId,
            JsonCredentials = jsonContent,
        }.Build();

        services.AddSingleton<IShoppingListRepository>(s =>
            new ShoppingListFireBaseRepository(
                s.GetRequiredService<IMapper>(),
                firestoreDb));


        return services;
        }
    }

