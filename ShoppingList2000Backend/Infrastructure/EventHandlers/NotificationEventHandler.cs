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
            var userIdAndEligibleUsers = new List<string>(shoppingListUpdatedEvent.ShoppingList.EligibleUsers);
            userIdAndEligibleUsers.Add(shoppingListUpdatedEvent.ShoppingList.CreatorUserId);
            foreach (var userId in userIdAndEligibleUsers)
            {
                var connectionIds = ShoppingListHub.Connections.GetConnections(userId);
                foreach (var connectionId in connectionIds)
                {
                      await _hubContext.Clients.Client(connectionId).SendAsync("UpdateShoppingList", shoppingListUpdatedEvent.ShoppingList);
                }
            }
        }
    }
}
