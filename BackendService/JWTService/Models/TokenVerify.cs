using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    //驗證 token時傳入的內容
    public class TokenVerify
    {
        public string AccessToken { set; get; } = "";

        public string RefreshTokeno { set; get; } = "";
    }
}