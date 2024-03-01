using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingList
    {
        public string ShoppingListId { get; set; }
        public string ShoppingListName { get; set; }
        public List<Product> Products { get; set; }
        public string CreatorUserId { get; set; }
        public List<string> EligebleUsers { get; set; }

    }
}
