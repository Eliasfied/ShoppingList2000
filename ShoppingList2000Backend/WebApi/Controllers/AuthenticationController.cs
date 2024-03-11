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

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var userId = await _authenticationService.RegisterAsync(userDTO);
            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginResponse = await _authenticationService.LoginAsync(loginRequest);
            return Ok(loginResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            _authenticationService.LogoutAsync();
            return Ok();
        }

        [HttpGet("geheim")]
        [Authorize]
        public string Get()
        {
            return "geheime Ressource";
        }
    }
}
