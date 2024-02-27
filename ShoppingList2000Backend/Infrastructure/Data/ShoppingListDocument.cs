using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    [FirestoreData]
    public class ShoppingListDocument
    {
        [FirestoreDocumentId]
        public string ShoppingListId { get; set; }

        [FirestoreProperty]
        public string ShoppingListName { get; set; }

        [FirestoreProperty]
        public List<ProductDocument> Products { get; set; }
    }
}
