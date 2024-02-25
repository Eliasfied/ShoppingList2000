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
        public async Task<string> RegisterAsync(string email, string password)
        {
            var userArgs = new UserRecordArgs { Email = email, Password = password };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

            return userRecord.Uid;
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            var userCredentials = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
            return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();
        }
    }
}
