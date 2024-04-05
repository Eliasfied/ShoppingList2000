

using Application.Interfaces.EventDispatcher;
using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;

namespace ShoppingList2000BackendTest;

[TestFixture]
public class NotificationServiceTest
{
    private Mock<INotificationRepository> _notificationRepositoryMock;
    private Mock<IEventDispatcher> _eventDispatcherMock;

    [SetUp]
    public void Setup()
    {
        _notificationRepositoryMock = new Mock<INotificationRepository>();
        _eventDispatcherMock = new Mock<IEventDispatcher>();
    }

    [Test]
    public async Task GetNotificationsForUser_ReturnsExpectedNotifications()
    {
        // Arrange
        var userId = "testUserId";
        var expectedNotifications = new List<Notification>
    {
         new Notification
            {
                Id = "1",
                Text = "Test",
                SenderName = "Sender",
                ReceiverEmail = "receiver@test.com",
                ReceiverId = "receiverId",
                Type = "Type",
                IsAcknowledged = false,
                Data = new Dictionary<string, object>(),
                Date = DateTime.Now
            },
              new Notification
            {
                Id = "2",
                Text = "Test2",
                SenderName = "Sender2",
                ReceiverEmail = "receive2r@test.com",
                ReceiverId = "receiverId22",
                Type = "Type",
                IsAcknowledged = false,
                Data = new Dictionary<string, object>(),
                Date = DateTime.Now
            }
    };

        _notificationRepositoryMock.Setup(repo => repo.GetNotificationsForUser(userId)).ReturnsAsync(expectedNotifications);

        var eventDispatcherMock = new Mock<IEventDispatcher>(); // macht nix

        var notificationService = new NotificationService(_notificationRepositoryMock.Object, eventDispatcherMock.Object);

        // Act
        var actualNotifications = await notificationService.GetNotificationsForUser(userId);

        // Assert
        Assert.That(actualNotifications, Is.EqualTo(expectedNotifications));
    }

    [Test]
    public async Task AcknowledgeNotification_ReturnsExpectedNotification()
    {
        // Arrange
        var notificationId = "testNotificationId";
        var expectedNotification = new Notification
        {
            Id = "3",
            Text = "Test3",
            SenderName = "Sender3",
            ReceiverEmail = "receive4442r@test.com",
            ReceiverId = "receiverId22222",
            Type = "Type",
            IsAcknowledged = false,
            Data = new Dictionary<string, object>(),
            Date = DateTime.Now
        };

        var notificationRepositoryMock = new Mock<INotificationRepository>();
        notificationRepositoryMock.Setup(repo => repo.AcknowledgeNotification(notificationId)).ReturnsAsync(expectedNotification);

        var eventDispatcherMock = new Mock<IEventDispatcher>();

        var notificationService = new NotificationService(notificationRepositoryMock.Object, eventDispatcherMock.Object);

        // Act
        var actualNotification = await notificationService.AcknowledgeNotification(notificationId);

        // Assert
        Assert.That(actualNotification, Is.EqualTo(expectedNotification));
    }

}
