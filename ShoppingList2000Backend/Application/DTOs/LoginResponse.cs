using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LoginResponse
    {
        public string? JwtToken { get; set; }
        public string? UserUid { get; set; }
        public string? Username { get; set; }
    }
}
