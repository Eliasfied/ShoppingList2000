using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetNotificationsForUser(string userId)
        {
            var notifications = await _notificationService.GetNotificationsForUser(userId);
            return Ok(notifications);
        }

        [HttpPost("acknowledge")]
        public async Task<IActionResult> AcknowledgeNotification(string notificationId)
        {
            var notification = await _notificationService.AcknowledgeNotification(notificationId);
            return Ok(notification);
        }

    }
}
