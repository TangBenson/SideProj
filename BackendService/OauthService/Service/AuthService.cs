using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OauthService.Service
{
    public class AuthService
    {
        public AuthService()
        {
        }
        internal static async Task<string> GetIdTokenAsync(string authorization_code)
        {
            await Task.Delay(1000);
            return "token";
        }
    }
}