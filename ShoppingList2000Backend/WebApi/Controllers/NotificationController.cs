using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotificationsForUser(string userId)
        {
            var notifications = await _notificationService.GetNotificationsForUser(userId);
            return Ok(notifications);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddNotification(NotificationDTO notificationDTO)
        {
              await _notificationService.ShareShoppingList(notificationDTO);

            return Ok();
        }

    }
}
