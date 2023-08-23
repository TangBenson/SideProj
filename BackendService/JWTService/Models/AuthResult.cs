using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    public class AuthResult
    {
        /// <summary>
        /// Token是否有成功產生狀態
        /// </summary>
        /// <value>布林值</value>
        public bool Result { set; get; } = false;

        /// <summary>
        /// AccessToken
        /// </summary>
        /// <value>字串</value>
        public string JwtToken { set; get; } = "";

        public string RefreshToken { set; get; } = "";
    }
}