using Application.Events;
using Application.Interfaces.EventHandlers;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventHandlers
{
    public class ShoppingListSharedNotifyHandler: IEventHandler<ShoppingListSharedEvent>
    {
        public void Handle(ShoppingListSharedEvent shoppingListSharedEvent)
        {
            Console.WriteLine("Notification versendet!");
        }

    }
}
