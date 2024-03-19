using Application.Events;
using Application.Interfaces.EventHandlers;
using Application.Interfaces.Repositories;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventHandlers
{
    public class ShoppingListSharedNotifyHandler: IEventHandler<ShoppingListSharedEvent>
    {
        private readonly IHubContext<ShoppingListHub> _hubContext;

        public ShoppingListSharedNotifyHandler(IHubContext<ShoppingListHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void Handle(ShoppingListSharedEvent shoppingListSharedEvent)
        {

            var connectionIds = ShoppingListHub.Connections.GetConnections(shoppingListSharedEvent.ReceiverId);
            foreach (var connectionId in connectionIds)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ShoppingListShared");
            }
        }

    }
}
