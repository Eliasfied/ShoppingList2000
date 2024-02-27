using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController: ControllerBase
    {
        IShoppingListService _shoppingListService;
        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateShoppingList(ShoppingListDTO shoppingListDTO)
        {
            var response = await _shoppingListService.CreateShoppingList(shoppingListDTO);
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetShoppingList(string id)
        {
            var response = await _shoppingListService.GetShoppingList(id);

            return Ok(response);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllShoppingList()
        {
            var response = await _shoppingListService.GetAllShoppingList();

            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateShoppingList(ShoppingListDTO shoppingListDTO, string id)
        {
            var response = await _shoppingListService.UpdateShoppingList(shoppingListDTO, id);

            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteShoppingList(string id)
        {
            var response = await _shoppingListService.DeleteShoppingList(id);

            return Ok(response);
        }

    }
}
