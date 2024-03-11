using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IShoppingListRepository
    {
        Task<ShoppingList> CreateShoppingList(ShoppingList shoppingList);
        Task<ShoppingList> GetShoppingList(string shoppingListId);
        Task<List<ShoppingList>> GetAllShoppingLists(string userId);
        public Task<ShoppingList> UpdateShoppingList(ShoppingList shoppingList);
        public Task<string> DeleteShoppingList(string shoppingListId);
    }
}
