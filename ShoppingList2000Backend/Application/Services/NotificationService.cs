using Application.DTOs;
using Application.Events;
using Application.Interfaces.EventDispatcher;
using Application.Interfaces.EventHandlers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public NotificationService(INotificationRepository notificationRepository, IEventDispatcher eventDispatcher)
        {
            _notificationRepository = notificationRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<List<Notification>> GetNotificationsForUser(string userId) {
            var notifications = await _notificationRepository.GetNotificationsForUser(userId);
            return notifications.ToList();
        }

        public async Task<Notification> AcknowledgeNotification(string notificationId)
        {
            var notification = await _notificationRepository.AcknowledgeNotification(notificationId);

            return notification;
        }

      
    }
}
