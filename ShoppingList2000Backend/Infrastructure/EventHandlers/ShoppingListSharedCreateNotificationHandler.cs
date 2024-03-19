using Application.Events;
using Application.Interfaces.EventHandlers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventHandlers
{
    public class ShoppingListSharedCreateNotificationHandler : IEventHandler<ShoppingListSharedEvent>
    {
        private readonly INotificationRepository _notificationRepository;

        public ShoppingListSharedCreateNotificationHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void Handle(ShoppingListSharedEvent shoppingListSharedEvent)
        {
            var notification = new Notification
            {
                SenderName = shoppingListSharedEvent.SenderName,
                ReceiverEmail = shoppingListSharedEvent.ReceiverEmail,
                ReceiverId = shoppingListSharedEvent.ReceiverId,
                Text = shoppingListSharedEvent.SenderName + " shared a ShoppingList with you!",
                IsAcknowledged = false,
                Type = "sharedShoppingList",
                Data = new Dictionary<string, object> { { "shoppingListId", shoppingListSharedEvent.ShoppingListId } },
                Date = DateTime.UtcNow
            };

            _notificationRepository.CreateNotification(notification);
        }
    }
}
