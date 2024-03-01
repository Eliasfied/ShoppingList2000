using Application.Events;
using Application.Interfaces.EventHandlers;
using Application.Mappings;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    public  class ShoppingListHub : Hub
    {
        private ConnectionMapping<string> _Connections = new ConnectionMapping<string>();

       

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; // Erhalten Sie die Benutzer-ID aus dem Authentifizierungstoken
            _Connections.Add(userId, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier; // Erhalten Sie die Benutzer-ID aus dem Authentifizierungstoken
            _Connections.Remove(userId, Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
