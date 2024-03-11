using Application.DTOs;
using Application.Events;
using Application.Interfaces.EventHandlers;
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
        private readonly List<IShoppingListUpdatedEventHandler> _updatedEventsHandler;

        public ShoppingListService(IMapper mapper, IShoppingListRepository shoppingListRepository, List<IShoppingListUpdatedEventHandler> updatedEventsHandler)
        {
            _mapper = mapper;
            _shoppingListRepository = shoppingListRepository;
            _updatedEventsHandler = updatedEventsHandler;
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
        public async Task<List<ShoppingListDTO>> GetAllShoppingLists(string userId)
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingLists(userId);

            return _mapper.Map<List<ShoppingListDTO>>(shoppingLists);
        }
        public async Task<ShoppingListDTO> UpdateShoppingList(ShoppingListDTO shoppingListDTO)
        {
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListDTO);
            var shoppingListBack = await _shoppingListRepository.UpdateShoppingList(shoppingList);
            var shoppingListDTOBack = _mapper.Map<ShoppingListDTO>(shoppingListBack);

            var shoppingListUpdatedEvent = new ShoppingListUpdatedEvent(shoppingListBack);

            foreach (var handler in _updatedEventsHandler)
            {
                handler.Handle(shoppingListUpdatedEvent);
            }


            return shoppingListDTOBack;
        }
        public Task<string> DeleteShoppingList(string shoppingListId)
        {
            return _shoppingListRepository.DeleteShoppingList(shoppingListId);
        }
    }
}
