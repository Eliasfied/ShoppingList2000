using Application.Interfaces.Services;
using Domain.Entities;
using WebApi.Controllers;

namespace ShoppingList2000BackendTest;

[TestFixture]
public class NotificationControllerTest
{
    private NotificationController _notificationController;
    private Mock<INotificationService> _notificationServiceMock;

    [SetUp]
    public void Setup()
    {
        _notificationServiceMock = new Mock<INotificationService>();
        _notificationController = new NotificationController(_notificationServiceMock.Object);
    }


    [Test]
    public async Task TestGetNotificationsForUser()
    {
        // Arrange
        string userId = "testUser";
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
            }
        };
        _notificationServiceMock.Setup(service => service.GetNotificationsForUser(userId)).ReturnsAsync(expectedNotifications);

        // Act
        var result = await _notificationController.GetNotificationsForUser(userId);

        // Assert

        // Überprüft, ob das Ergebnis der Methode nicht null ist. Wenn es null ist, schlägt der Test fehl.
        Assert.IsNotNull(result);

        // Versucht, das Ergebnis in ein OkObjectResult zu konvertieren. OkObjectResult ist eine Art von HTTP-Antwort in ASP.NET Core, die einen HTTP 200 OK Statuscode repräsentiert.
        var okResult = result as OkObjectResult;

        // Überprüft, ob die Konvertierung erfolgreich war und okResult nicht null ist. Wenn es null ist, schlägt der Test fehl.
        Assert.IsNotNull(okResult);

        // Versucht, den Wert des OkObjectResult (den Inhalt der HTTP-Antwort) in eine Liste von Benachrichtigungen zu konvertieren.
        var actualNotifications = okResult.Value as List<Notification>;

        // Überprüft, ob die tatsächlich zurückgegebene Liste von Benachrichtigungen der erwarteten Liste von Benachrichtigungen entspricht. Wenn sie nicht gleich sind, schlägt der Test fehl.
        Assert.That(actualNotifications, Is.EqualTo(expectedNotifications));
    }

     [Test]
    public async Task TestAcknowledgeNotification()
    {
        // Arrange
        string notificationId = "testNotification";
        var expectedNotification = new Notification 
        { 
            Id = notificationId, 
            Text = "Test", 
            SenderName = "Sender", 
            ReceiverEmail = "receiver@test.com", 
            ReceiverId = "receiverId", 
            Type = "Type", 
            IsAcknowledged = true, 
            Data = new Dictionary<string, object>(), 
            Date = DateTime.Now 
        };
        _notificationServiceMock.Setup(service => service.AcknowledgeNotification(notificationId)).ReturnsAsync(expectedNotification);

        // Act
        var result = await _notificationController.AcknowledgeNotification(notificationId);

        // Assert
        Assert.IsNotNull(result);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var actualNotification = okResult.Value as Notification;
        Assert.That(actualNotification, Is.EqualTo(expectedNotification));
    }


}
