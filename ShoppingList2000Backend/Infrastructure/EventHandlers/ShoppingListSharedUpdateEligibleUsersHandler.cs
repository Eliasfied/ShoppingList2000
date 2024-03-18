using Application.Events;
using Application.Interfaces.EventHandlers;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventHandlers
{
    public class ShoppingListSharedUpdateEligibleUsersHandler : IEventHandler<ShoppingListSharedEvent>
    {
        private readonly IShoppingListRepository _repository;
        public ShoppingListSharedUpdateEligibleUsersHandler(IShoppingListRepository shoppingListRepository) {
            _repository = shoppingListRepository;
        }
        public async void Handle(ShoppingListSharedEvent shoppingListSharedEvent)
        {
            var shoppinglist = await _repository.GetShoppingList(shoppingListSharedEvent.ShoppingListId);
            shoppinglist.EligibleUsers.Add(shoppingListSharedEvent.ReceiverId);

            await _repository.UpdateShoppingList(shoppinglist);
        }
    }
}
