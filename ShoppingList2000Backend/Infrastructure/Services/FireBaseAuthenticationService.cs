using Application.DTOs;
using Application.Interfaces.Services;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FireBaseAuthenticationService : IAuthenticationService
    {

        private readonly FirebaseAuthClient _firebaseAuth;
        public FireBaseAuthenticationService(FirebaseAuthClient firebaseAuth)
        {
            _firebaseAuth = firebaseAuth;
        }
        public async Task<string> RegisterAsync(UserDTO userDTO)
        {
            var userArgs = new UserRecordArgs { Email = userDTO.Email, Password = userDTO.Password };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

            return userRecord.Uid;
        }
        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            var userCredentials = await _firebaseAuth.SignInWithEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);
            return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();
        }

        public async Task LogoutAsync()
        {
              _firebaseAuth.SignOut();
        }
    }
}
