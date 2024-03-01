using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShoppingListDTO
    {
        private string _shoppingListId;
        public string ShoppingListId
        {
            get => _shoppingListId;
            set => _shoppingListId = value ?? Guid.NewGuid().ToString();
        }

        [Required]
        public string ShoppingListName { get; set; }

        [Required]
        public List<Product> Products { get; set; }

        [Required]
        public string CreatorUserId { get; set; }

        public List<string> EligebleUsers { get; set; }
    }
}
