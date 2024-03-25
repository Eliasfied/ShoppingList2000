using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(UserDTO userDTO);

        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);

        Task LogoutAsync();
        Task<string> LoginWithGoogle(string googleToken);
        
    }
}
