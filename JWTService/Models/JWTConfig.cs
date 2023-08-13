using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    public class JWTConfig
    {
        /// <summary>
        /// 發行者
        /// </summary>
        public string Issuer { set; get; } = "";

        /// <summary>
        /// 加密金鑰
        /// </summary>
        public string SignKey { set; get; } = "";

        /// <summary>
        /// 設置Token存活多久(分鐘)
        /// </summary>
        // public int ExpireDateTime { set; get; }
    }
}