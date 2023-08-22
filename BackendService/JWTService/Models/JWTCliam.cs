using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Models
{
    // 這裡是她媽的 PayLoad,前七種是JWT標準公定的Registered claims,建議放入,不爽放也可以,妳她媽爽就好
    public class JWTCliam
    {
        /// <summary>
        /// 發行者、簽發人
        /// </summary>
        public string Iss { set; get; } = "";

        /// <summary>
        /// User內容、主題
        /// </summary>
        public string Sub { set; get; } = "";

        /// <summary>
        /// 接收者、接收人
        /// </summary>
        public string Aud { set; get; } = "";

        /// <summary>
        /// 有效期限、過期時間
        /// </summary>
        public string Exp { set; get; } = "";

        /// <summary>
        /// 起始時間、開始生效時間
        /// </summary>
        public string Nbf { set; get; } = "";

        /// <summary>
        /// 發行時間、簽發時間
        /// </summary>
        public string Iat { set; get; } = "";

        /// <summary>
        /// 獨立識別ID、編號(JWT的Id)
        /// </summary>
        public string Jti { set; get; } = "";

        // 訪客模式,0為訪客、1為會員
        public string CustMode { set; get; } = "0";
        // 會員姓名
        public string CustName { set; get; } = "無名氏";
    }
}