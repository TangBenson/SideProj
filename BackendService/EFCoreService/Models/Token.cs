using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreService.Models
{
    public class Token
    {
        public string Account { get; set; } = "";
        public string AccessToken { get; set; } = "";
        public string RefreshTokeno { get; set; } = "";
        public DateTime ExpireTime { get; set; }
        // public DateTime RefreshExpireTime { get; set; }
    }
}