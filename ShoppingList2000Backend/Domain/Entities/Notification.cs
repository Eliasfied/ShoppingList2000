﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notification
    {
        public string Id { get; set; }
        public string SenderName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverId { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public bool IsAcknowledged { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public DateTime Date { get; set; }

    }

}
