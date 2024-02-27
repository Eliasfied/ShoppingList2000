using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ShoppingListService : IShoppingListService
    {
        IMapper _mapper;
        IShoppingListRepository _shoppingListRepository;
        public ShoppingListService(IMapper mapper, IShoppingListRepository shoppingListRepository)
        {
            _mapper = mapper;
            _shoppingListRepository = shoppingListRepository;
        }
        public async Task<ShoppingListDTO> CreateShoppingList(ShoppingListDTO shoppingListDTO)
        {
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListDTO);
            var shoppingListBack = await _shoppingListRepository.CreateShoppingList(shoppingList);

            var shoppingListDTOBack = _mapper.Map<ShoppingListDTO>(shoppingListBack);
            
            return shoppingListDTOBack;
        }
        public async Task<ShoppingListDTO> GetShoppingList(string shoppingListId)
        {
            var shoppingList = await _shoppingListRepository.GetShoppingList(shoppingListId);

            var shoppingListDTO = _mapper.Map<ShoppingListDTO>(shoppingList);

            return shoppingListDTO;

            
        }
        public async Task<List<ShoppingListDTO>> GetAllShoppingList()
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingList();

            return _mapper.Map<List<ShoppingListDTO>>(shoppingLists);
        }
        public async Task<ShoppingListDTO> UpdateShoppingList(ShoppingListDTO shoppingListDTO, string shoppingListId)
        {
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListDTO);
            var shoppingListBack = await _shoppingListRepository.UpdateShoppingList(shoppingList, shoppingListId);
            var shoppingListDTOBack = _mapper.Map<ShoppingListDTO>(shoppingListBack);

            return shoppingListDTOBack;
        }
        public Task<string> DeleteShoppingList(string shoppingListId)
        {
            return _shoppingListRepository.DeleteShoppingList(shoppingListId);
        }
    }
}
