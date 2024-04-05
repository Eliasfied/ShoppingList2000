using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using WebApi.Controllers;

namespace ShoppingList2000BackendTest;

[TestFixture]
public class AuthenticationControllerTest
{
    private Mock<IAuthenticationService> _authenticationServiceMock;
    private AuthenticationController _controller;

    [SetUp]
    public void SetUp()
    {
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _controller = new AuthenticationController(_authenticationServiceMock.Object, Mock.Of<ILogger<AuthenticationController>>());
    }

    [Test]
    public async Task Register_ReturnsOkResultWithUserId()
    {
        // Arrange
        var userDTO = new UserDTO();
        var expectedUserId = "testUserId";
        _authenticationServiceMock.Setup(service => service.RegisterAsync(userDTO)).ReturnsAsync(expectedUserId);

        // Act
        var result = await _controller.Register(userDTO);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var actualUserId = okResult.Value as string;
        Assert.That(actualUserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public async Task Login_ReturnsOkResultWithLoginResponse()
    {
        // Arrange
        var loginRequest = new LoginRequest();
        var expectedLoginResponse = new LoginResponse();
        _authenticationServiceMock.Setup(service => service.LoginAsync(loginRequest)).ReturnsAsync(expectedLoginResponse);

        // Act
        var result = await _controller.Login(loginRequest);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var actualLoginResponse = okResult.Value as LoginResponse;
        Assert.That(actualLoginResponse, Is.EqualTo(expectedLoginResponse));
    }

    [Test]
    public async Task Logout_ReturnsOkResult()
    {
        // Arrange
        _authenticationServiceMock.Setup(service => service.LogoutAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Logout();

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }


}
