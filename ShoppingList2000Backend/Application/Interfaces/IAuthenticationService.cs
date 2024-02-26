using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(UserDTO userDTO);

        Task<string> LoginAsync(LoginRequest loginRequest);
    }
}
