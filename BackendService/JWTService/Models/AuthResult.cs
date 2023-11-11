using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    // 建立 token時 response的內容
    public class AuthResult : TokenVerify
    {
        /// Token是否有成功產生狀態
        public bool Result { set; get; } = false;
    }
}