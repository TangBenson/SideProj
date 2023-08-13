using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JWTService.Services
{
    public class JwtAuthService2
    {
        //金鑰，從設定檔或資料庫取得
        public readonly string _key;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        public JwtAuthService2(IConfiguration configuration, IHttpContextAccessor httpContext,string a)
        {
            _config = configuration;
            _key = _config.GetSection("JwtSettings")["SignKey"]!.ToString();
            _httpContext = httpContext;
            Console.WriteLine(a);
        }

        //產生 jwt Token
        public Token Create(string user)
        {
            Console.WriteLine(_key);
            var exp = 3600;   //過期時間(秒)

            //稍微修改 Payload 將使用者資訊和過期時間分開
            var payload = new Payload
            {
                info = user,
                //Unix 時間戳
                exp = Convert.ToInt32(
                    (DateTime.Now.AddSeconds(exp) -
                     new DateTime(2023, 8, 1)).TotalSeconds)
            };

            var payloadJson = JsonSerializer.Serialize(payload);
            var payloadBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payloadJson));
            var iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);

            //使用 AES 加密 Payload
            var payloadEncrypt = AESEncrypt(payloadBase64, _key.Substring(0, 16), iv);

            //取得簽章，把 header拿來放 iv
            var signature = ComputeHMACSHA256(iv + "." + payloadEncrypt, _key.Substring(0, 16));

            return new Token
            {
                //Token 為 iv + encrypt + signature，並用 . 串聯
                access_token = iv + "." + payloadEncrypt + "." + signature,
                //Refresh Token 使用 Guid 產生
                refresh_token = Guid.NewGuid().ToString().Replace("-", ""),
                expires_in = exp,
            };
        }

        //驗證 jwt Tokek
        public string Validate()
        {
            var token = _httpContext.HttpContext!.Request.Headers["Authorization"];

            var split = token.FirstOrDefault().Split('.');
            var iv = split[0];
            var encrypt = split[1];
            var signature = split[2];

            //檢查簽章是否正確
            if (signature != ComputeHMACSHA256(iv + "." + encrypt, _key.Substring(0, 64)))
            {
                return null;
            }

            //使用 AES 解密 Payload
            var base64 = AESDecrypt(encrypt, _key.Substring(0, 16), iv);
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            var payload = JsonSerializer.Deserialize<Payload>(json);

            //檢查是否過期
            if (payload.exp < Convert.ToInt32(
                (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds))
            {
                return null;
            }

            return payload.info;
        }



        //產生 HMACSHA256 雜湊
        public static string ComputeHMACSHA256(string data, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hmacSHA = new HMACSHA256(keyBytes))
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);
                var hash = hmacSHA.ComputeHash(dataBytes, 0, dataBytes.Length);
                return BitConverter.ToString(hash).Replace("-", "").ToUpper();
            }
        }

        //AES 加密
        public static string AESEncrypt(string data, string key, string iv)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var ivBytes = Encoding.UTF8.GetBytes(iv);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                var encryptor = aes.CreateEncryptor();
                var encrypt = encryptor
                    .TransformFinalBlock(dataBytes, 0, dataBytes.Length);
                return Convert.ToBase64String(encrypt);
            }
        }

        //AES 解密
        public static string AESDecrypt(string data, string key, string iv)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var ivBytes = Encoding.UTF8.GetBytes(iv);
            var dataBytes = Convert.FromBase64String(data);
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                var decryptor = aes.CreateDecryptor();
                var decrypt = decryptor
                    .TransformFinalBlock(dataBytes, 0, dataBytes.Length);
                return Encoding.UTF8.GetString(decrypt);
            }
        }
    }

    //定義回傳的 Token 結構
    public class Token
    {
        public string access_token { get; set; } = "";
        public string refresh_token { get; set; } = "";
        public int expires_in { get; set; }
    }
    public class Payload
    {
        //使用者資訊
        public string info { get; set; } = "";
        //過期時間
        public int exp { get; set; }
    }
}