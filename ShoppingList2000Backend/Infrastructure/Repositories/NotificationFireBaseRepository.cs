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
        public async Task<Notification> CreateNotification(Notification notification) {

            var collection = _firestoreDb.Collection(_collectionName);
            var notificationDocument = _mapper.Map<NotificationDocument>(notification);
            var notificationBackReference = await collection.AddAsync(notificationDocument);
            var notificationBack = _mapper.Map<Notification>(notificationBackReference);
            Console.WriteLine("Notification in db added");
            return notificationBack;
        }

        public async Task<List<Notification>> GetNotificationsForUser(string userId)
        {
            var collection = _firestoreDb.Collection(_collectionName);
            var receiverQuery = collection.WhereEqualTo("ReceiverId", userId);
            var receiverSnapshot = await receiverQuery.GetSnapshotAsync();
            var notificationDocuments = receiverSnapshot.Documents
                .Select(documentSnapshot => documentSnapshot.ConvertTo<NotificationDocument>())
                .ToList();

            var notifications = _mapper.Map<List<Notification>>(notificationDocuments);


            return notifications;
        }

        public async Task<Notification> AcknowledgeNotification(string notificationId)
        {
            var documentReference = _firestoreDb.Collection(_collectionName).Document(notificationId);
            var snapshot = await documentReference.GetSnapshotAsync();
            var notificationDocument = snapshot.ConvertTo<NotificationDocument>();
            notificationDocument.IsAcknowledged = true;

            await documentReference.SetAsync(notificationDocument);

            var notificationBack = _mapper.Map<Notification>(notificationDocument);


            return notificationBack;



        }


    }
}
