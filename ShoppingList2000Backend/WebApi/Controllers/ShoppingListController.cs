﻿using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetShoppingList(string shoppingListId, string userId)
        {
            var response = await _shoppingListService.GetShoppingList(shoppingListId);

            return Ok(response);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllShoppingLists(string userId)
        {
            var response = await _shoppingListService.GetAllShoppingLists(userId);

            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateShoppingList(ShoppingListDTO shoppingListDTO, string userId)
        {
            var response = await _shoppingListService.UpdateShoppingList(shoppingListDTO);

            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteShoppingList(string shoppingListId, string userId)
        {
            var response = await _shoppingListService.DeleteShoppingList(shoppingListId);

            return Ok(response);
        }

        [HttpPost("share")]
        public async Task<IActionResult> ShareNotification([FromBody] ShareNotificationRequest request)
        {
            await _shoppingListService.ShareShoppingList(request.SenderName,request.ReceiverEmail, request.ShoppingListId);

            return Ok();
        }

    }
}
