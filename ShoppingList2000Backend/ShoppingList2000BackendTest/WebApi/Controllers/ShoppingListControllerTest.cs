using WebApi.Controllers;
using Application.Interfaces.Services;
using Application.DTOs;
using Domain.Entities;

namespace ShoppingList2000BackendTest;

[TestFixture]
public class ShoppingListControllerTest
{
    private Mock<IShoppingListService> _shoppingListServiceMock;
    private ShoppingListController _controller;


    [SetUp]
    public void SetUp()
    {
        _shoppingListServiceMock = new Mock<IShoppingListService>();
        _controller = new ShoppingListController(_shoppingListServiceMock.Object);
    }

    [Test]
    public async Task CreateShoppingList_ReturnsOkResultWithExpectedShoppingList()
    {
        // Arrange
        var shoppingListDTO = new ShoppingListDTO
        {
            ShoppingListId = Guid.NewGuid().ToString(),
            ShoppingListName = "Test Shopping List",
            Products = new List<Product>(),
            CreatorUserId = "testUser",
            EligibleUsers = new List<string>(),
            LastUpdatedUser = "testUser"
        };
        _shoppingListServiceMock.Setup(service => service.CreateShoppingList(shoppingListDTO)).ReturnsAsync(shoppingListDTO);

        // Act
        var result = await _controller.CreateShoppingList(shoppingListDTO);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var actualShoppingList = okResult.Value as ShoppingListDTO;
        Assert.That(actualShoppingList, Is.EqualTo(shoppingListDTO));
    }

    [Test]
public async Task GetShoppingList_ReturnsOkResultWithExpectedShoppingList()
{
    // Arrange
    var shoppingListId = "testId";
    var userId = "testUserId";
    var shoppingListDTO = new ShoppingListDTO();
    _shoppingListServiceMock.Setup(service => service.GetShoppingList(shoppingListId)).ReturnsAsync(shoppingListDTO);

    // Act
    var result = await _controller.GetShoppingList(shoppingListId, userId);

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result);
    var okResult = result as OkObjectResult;
    Assert.IsNotNull(okResult);
    var actualShoppingList = okResult.Value as ShoppingListDTO;
    Assert.That(actualShoppingList, Is.EqualTo(shoppingListDTO));
}

[Test]
public async Task GetAllShoppingLists_ReturnsOkResultWithExpectedShoppingLists()
{
    // Arrange
    var userId = "testUserId";
    var shoppingLists = new List<ShoppingListDTO>();
    _shoppingListServiceMock.Setup(service => service.GetAllShoppingLists(userId)).ReturnsAsync(shoppingLists);

    // Act
    var result = await _controller.GetAllShoppingLists(userId);

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result);
    var okResult = result as OkObjectResult;
    Assert.IsNotNull(okResult);
    var actualShoppingLists = okResult.Value as List<ShoppingListDTO>;
    Assert.That(actualShoppingLists, Is.EqualTo(shoppingLists));
}

[Test]
public async Task UpdateShoppingList_ReturnsOkResultWithUpdatedShoppingList()
{
    // Arrange
    var shoppingListDTO = new ShoppingListDTO();
    var userId = "testUserId";
    _shoppingListServiceMock.Setup(service => service.UpdateShoppingList(shoppingListDTO)).ReturnsAsync(shoppingListDTO);

    // Act
    var result = await _controller.UpdateShoppingList(shoppingListDTO, userId);

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(result);
    var okResult = result as OkObjectResult;
    Assert.IsNotNull(okResult);
    var actualShoppingList = okResult.Value as ShoppingListDTO;
    Assert.That(actualShoppingList, Is.EqualTo(shoppingListDTO));
}

[Test]
public async Task DeleteShoppingList_ReturnsOkResultWithDeletedShoppingList()
{
    // Arrange
    var shoppingListId = "testId";
    var userId = "testUserId";
    var expectedResponse = "Success";
    _shoppingListServiceMock.Setup(service => service.DeleteShoppingList(shoppingListId)).ReturnsAsync(expectedResponse);

    // Act
    var result = await _controller.DeleteShoppingList(shoppingListId, userId);

    // Assert
     Assert.IsInstanceOf<OkObjectResult>(result);
    var okResult = result as OkObjectResult;
    Assert.IsNotNull(okResult);
    var actualResponse = okResult.Value as string;
    Assert.That(actualResponse, Is.EqualTo(expectedResponse));
}
}


