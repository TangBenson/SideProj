using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    public class TokenRequest
    {
        public string JwtToken { set; get; } = "";

        public string RefreshToken { set; get; } = "";
    }
}