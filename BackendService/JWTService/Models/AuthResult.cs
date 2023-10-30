using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    public class AuthResult:TokenRequest
    {
        /// Token是否有成功產生狀態
        public bool Result { set; get; } = false;
    }
}