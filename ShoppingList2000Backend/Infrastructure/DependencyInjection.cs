﻿using Application.Interfaces.EventHandlers;
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
using Infrastructure.EventHandlers;
using Infrastructure.Hubs;
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


        services.AddAutoMapper(assembly, typeof(ShoppingListProfileInfrastructure).Assembly);
        services.AddSignalRCore();

        services.AddSingleton<IAuthenticationService, FireBaseAuthenticationService>();

        services.AddTransient<ShoppingListHub>();
        services.AddTransient<List<IShoppingListUpdatedEventHandler>>(provider =>
        {
            return new List<IShoppingListUpdatedEventHandler>
            {
                provider.GetRequiredService<NotificationEventHandler>()
            };
        });
        
        services.AddTransient<NotificationEventHandler>();

        //FireBase
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("firebase.json")
        });
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

