using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events
{
    public class ShoppingListSharedEvent
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string ShoppingListId { get; set; }
    }
}
