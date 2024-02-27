using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShoppingListDTO
    {
        public string ShoppingListId { get; set; }

        public string ShoppingListName { get; set; }
        public List<Product> Products { get; set; }
    }
}
