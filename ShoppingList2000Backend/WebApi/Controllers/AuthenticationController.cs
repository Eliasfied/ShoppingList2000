using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var userId = await _authenticationService.RegisterAsync(userDTO);
            _logger.LogInformation($"Benutzer mit der ID {userId} wurde erfolgreich registriert.");
            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var loginResponse = await _authenticationService.LoginAsync(loginRequest);
                _logger.LogInformation($"Benutzer mit der E-Mail {loginRequest.Email} hat sich erfolgreich angemeldet.");
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fehler beim Anmelden des Benutzers mit der E-Mail {loginRequest.Email}.");
                throw;
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return Ok();
        }

      

    }
}
