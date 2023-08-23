using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    public class UserToken
    {
        public string Account { set; get; } = "";
        public string Token { set; get; } = "";
        public string RefreshToken { set; get; } = "";
        
    }
}