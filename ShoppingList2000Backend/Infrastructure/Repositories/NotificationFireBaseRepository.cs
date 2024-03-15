using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Google.Cloud.Firestore;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NotificationFireBaseRepository : INotificationRepository
    {
        IMapper _mapper;
        private readonly FirestoreDb _firestoreDb;
        private const string _collectionName = "Notification";

        public NotificationFireBaseRepository(IMapper mapper, FirestoreDb firestoreDb)
        {
            _mapper = mapper;
            _firestoreDb = firestoreDb;
        }
        public async Task<Notification> AddNotification(Notification notification) {

            var collection = _firestoreDb.Collection(_collectionName);
            var notificationDocument = _mapper.Map<NotificationDocument>(notification);
            var notificationBackReference = await collection.AddAsync(notificationDocument);

            Console.WriteLine("Notification in db added");
            return notification;
        }

        public Task<List<Notification>> GetNotificationsForUser(string userId)
        {
            var notifications = new List<Notification>();

            return Task.FromResult(notifications);
        }

    }
}
