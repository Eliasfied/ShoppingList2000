using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotification(Notification notification);
        Task<List<Notification>> GetNotificationsForUser(string userId);
        Task<Notification> AcknowledgeNotification(string notificationId);

    }
}
