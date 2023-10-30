using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    public class UserToken : TokenRequest
    {
        public string Account { set; get; } = "";
        public DateTime Exp { set; get; }

    }
}