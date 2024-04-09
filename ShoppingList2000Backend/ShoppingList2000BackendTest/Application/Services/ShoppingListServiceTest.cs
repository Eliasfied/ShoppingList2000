using Application.DTOs;
using Application.Interfaces.EventDispatcher;
using Application.Interfaces.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;

namespace ShoppingList2000BackendTest;

[TestFixture]
public class ShoppingListServiceTest
{
    private Mock<IShoppingListRepository> _shoppingListRepositoryMock;
    private Mock<IEventDispatcher> _eventDispatcherMock;
    private Mock<IMapper> _mapperMock;
    private ShoppingListService _shoppingListService;
    [SetUp]
    public void Setup()
    {
        _shoppingListRepositoryMock = new Mock<IShoppingListRepository>();
        _eventDispatcherMock = new Mock<IEventDispatcher>();
        _mapperMock = new Mock<IMapper>();
        _shoppingListService = new ShoppingListService(_mapperMock.Object, _shoppingListRepositoryMock.Object, _eventDispatcherMock.Object);

    }

    [Test]
    public async Task CreateShoppingList_ShouldReturnShoppingListDTO()
    {
        // Arrange
        var shoppingListDTO = new ShoppingListDTO();
        var shoppingList = new ShoppingList();
        _mapperMock.Setup(m => m.Map<ShoppingList>(It.IsAny<ShoppingListDTO>())).Returns(shoppingList);
        _shoppingListRepositoryMock.Setup(s => s.CreateShoppingList(It.IsAny<ShoppingList>())).ReturnsAsync(shoppingList);
        _mapperMock.Setup(m => m.Map<ShoppingListDTO>(It.IsAny<ShoppingList>())).Returns(shoppingListDTO);

        // Act
        var result = await _shoppingListService.CreateShoppingList(shoppingListDTO);

        // Assert
        Assert.That(result, Is.EqualTo(shoppingListDTO));
    }

    [Test]
    public async Task GetShoppingList_ShouldReturnShoppingListDTO()
    {
        // Arrange
        var shoppingListId = "testId";
        var shoppingList = new ShoppingList();
        var shoppingListDTO = new ShoppingListDTO();
        _shoppingListRepositoryMock.Setup(s => s.GetShoppingList(It.IsAny<string>())).ReturnsAsync(shoppingList);
        _mapperMock.Setup(m => m.Map<ShoppingListDTO>(It.IsAny<ShoppingList>())).Returns(shoppingListDTO);

        // Act
        var result = await _shoppingListService.GetShoppingList(shoppingListId);

        // Assert
        Assert.That(result, Is.EqualTo(shoppingListDTO));
    }

    [Test]
    public async Task UpdateShoppingList_ShouldReturnUpdatedShoppingListDTO()
    {
        // Arrange
        var shoppingListDTO = new ShoppingListDTO();
        var updatedShoppingListDTO = new ShoppingListDTO();
        var shoppingList = new ShoppingList();
        _mapperMock.Setup(m => m.Map<ShoppingList>(It.IsAny<ShoppingListDTO>())).Returns(shoppingList);
        _shoppingListRepositoryMock.Setup(s => s.UpdateShoppingList(It.IsAny<ShoppingList>())).ReturnsAsync(shoppingList);
        _mapperMock.Setup(m => m.Map<ShoppingListDTO>(It.IsAny<ShoppingList>())).Returns(updatedShoppingListDTO);

        // Act
        var result = await _shoppingListService.UpdateShoppingList(shoppingListDTO);

        // Assert
        Assert.That(result, Is.EqualTo(updatedShoppingListDTO));
    }

    [Test]
    public async Task DeleteShoppingList_ShouldReturnShoppingListIdWhenSuccessful()
    {
        // Arrange
        var shoppingListId = "testId";
        _shoppingListRepositoryMock.Setup(s => s.DeleteShoppingList(It.IsAny<string>())).ReturnsAsync(shoppingListId);

        // Act
        var result = await _shoppingListService.DeleteShoppingList(shoppingListId);

        // Assert
        Assert.That(result, Is.EqualTo(shoppingListId));
    }

   [Test]
public async Task ShareShoppingList_ShouldNotThrowException()
{
    // Arrange
    var senderName = "testSender";
    var receiverEmail = "testReceiver@test.com";
    var shoppingListId = "testId";
    _shoppingListRepositoryMock.Setup(s => s.GetUserIdWithEmail(It.IsAny<string>())).ReturnsAsync("testReceiverId");

    // Act & Assert
    Assert.DoesNotThrowAsync(async () => await _shoppingListService.ShareShoppingList(senderName, receiverEmail, shoppingListId));
}


}
