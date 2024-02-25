using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register(string email, string password)
        {
            var userId = await _authenticationService.RegisterAsync(email, password);
            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var jwtToken = await _authenticationService.LoginAsync(email, password);
            return Ok(jwtToken);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
