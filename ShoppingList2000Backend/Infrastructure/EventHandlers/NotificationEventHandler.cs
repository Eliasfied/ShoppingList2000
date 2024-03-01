using Application.Events;
using Application.Interfaces.EventHandlers;
using Domain.Entities;
using Firebase.Auth;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventHandlers
{
    public class NotificationEventHandler : IShoppingListUpdatedEventHandler
    {
        private readonly IHubContext<ShoppingListHub> _hubContext;

        public NotificationEventHandler(IHubContext<ShoppingListHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void Handle(ShoppingListUpdatedEvent shoppingListUpdatedEvent)
        {
            foreach (var userId in shoppingListUpdatedEvent.ShoppingList.EligebleUsers)
            {
                await _hubContext.Clients.Client(userId).SendAsync("ShoppingListUpdated");
            }
        }
    }
}
