using Application.Events;
using Application.Interfaces.EventHandlers;
using Application.Mappings;
using Domain.Entities;
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
        private static ConnectionMapping<string> _Connections = new ConnectionMapping<string>();
        public static ConnectionMapping<string> Connections
        {
            get { return _Connections; }
        }


        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(Context);
            var userId = Context.UserIdentifier;
            _Connections.Add(userId, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier; 
            _Connections.Remove(userId, Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateShoppingList(ShoppingList shoppingList)
        {
            try
            {
                await Clients.All.SendAsync("UpdateShoppingList", shoppingList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
