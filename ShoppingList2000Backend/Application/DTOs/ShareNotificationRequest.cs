using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShareNotificationRequest
    {
        public string SenderName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ShoppingListId { get; set; }
    }
}
