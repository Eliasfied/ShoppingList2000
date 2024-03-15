using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    [FirestoreData]
    public class NotificationDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string SenderId { get; set; }

        [FirestoreProperty]
        public string ReceiverId { get; set; }

        [FirestoreProperty]
        public string Type { get; set; }

        [FirestoreProperty]
        public string Status { get; set; }

    //    [FirestoreProperty]
     //   public Dictionary<string, object> Data { get; set; }
    }
}
