using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NotificationFireBaseRepository : INotificationRepository
    {
        public Task AddNotification(Notification notification) { return Task.CompletedTask; }

        public Task<List<Notification>> GetNotificationsForUser(string userId)
        {
            var notifications = new List<Notification>();

            return Task.FromResult(notifications);
        }

    }
}
