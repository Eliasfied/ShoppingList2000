﻿using Application.Events;
using Application.Interfaces.EventHandlers;
using Application.Interfaces.Services;
using Application.Mappings;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) {

        var assembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(assembly);

        services.AddAutoMapper(assembly, typeof(ShoppingListProfileApplication).Assembly);

        services.AddSingleton<IShoppingListService, ShoppingListService>();
        services.AddSingleton<INotificationService, NotificationService>();



        return services;
        }
    }

