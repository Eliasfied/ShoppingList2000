using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IShoppingListService
    {
        Task<ShoppingListDTO> CreateShoppingList(ShoppingListDTO shoppingList);
        Task<ShoppingListDTO> GetShoppingList(string shoppingListId);
        Task<List<ShoppingListDTO>> GetAllShoppingList();
        Task<ShoppingListDTO> UpdateShoppingList(ShoppingListDTO shoppingList, string shoppingListId);
        Task<string> DeleteShoppingList(string shoppingListId);

    }
}
