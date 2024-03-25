using Application.Events;
using Application.Interfaces.EventHandlers;
using Application.Interfaces.Repositories;
using Google.Apis.Logging;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EventHandlers
{
    public class ShoppingListSharedNotifyHandler: IEventHandler<ShoppingListSharedEvent>
    {
        private readonly IHubContext<ShoppingListHub> _hubContext;
        private readonly ILogger<ShoppingListSharedNotifyHandler> _logger;

        public ShoppingListSharedNotifyHandler(IHubContext<ShoppingListHub> hubContext, ILogger<ShoppingListSharedNotifyHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async void Handle(ShoppingListSharedEvent shoppingListSharedEvent)
        {

            var connectionIds = ShoppingListHub.Connections.GetConnections(shoppingListSharedEvent.ReceiverId);
            foreach (var connectionId in connectionIds)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ShoppingListShared");
                _logger.LogInformation("<ShoppinglistShared> Event successfully send to Client with Name: " + shoppingListSharedEvent.SenderName);
            }
        }

    }
}
