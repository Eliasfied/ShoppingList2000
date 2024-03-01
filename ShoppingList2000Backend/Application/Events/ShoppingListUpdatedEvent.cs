using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events
{
    public class ShoppingListUpdatedEvent
    {
        public ShoppingList ShoppingList { get; set; }

        public ShoppingListUpdatedEvent(ShoppingList shoppingList)
        {
         ShoppingList = shoppingList;
        }
    }
}
