using Application.DTOs;
using Application.Events;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FireBaseAuthenticationService : IAuthenticationService
    {
        FirebaseAuthClient _firebaseAuth;
        public FireBaseAuthenticationService(FirebaseAuthClient firebaseAuth)
        {
            _firebaseAuth = firebaseAuth;
        }

        public async Task<string> RegisterAsync(UserDTO userDTO)
        {
            var userArgs = new UserRecordArgs { Email = userDTO.Email, Password = userDTO.Password, DisplayName = userDTO.Name };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

            return userRecord.Uid;
        }


        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var userCredentials = await _firebaseAuth.SignInWithEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);
            if (userCredentials == null)
            {
                return null;
            }

            var jwtToken = await userCredentials.User.GetIdTokenAsync();
            var userUid = userCredentials.User.Uid;
            var username = userCredentials.User.Info.DisplayName;

            return new LoginResponse
            {
                JwtToken = jwtToken,
                UserUid = userUid,
                Username = username
                
            };
        }

        public async Task LogoutAsync()
        {
              _firebaseAuth.SignOut();
        }

        public async Task<string> LoginWithGoogle(string googleToken)
        {
            FirebaseToken decodedToken;
            try
            {
                decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(googleToken);
            }
            catch (Exception ex)
            {
                // Handle token verification failure
                throw new Exception("Failed to verify Google token", ex);
            }

            return "token valide!";
        }

    }
}
