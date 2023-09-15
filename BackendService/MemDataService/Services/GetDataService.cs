using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemDataService.Services
{
    public class GetDataService : IGetDataService
    {
        private string mytoken = "";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetDataService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public (string ID, string Name, string Email, string Phone) GetUserData()
        {
            #region 驗證 JwtToken並轉成 IDNO
            //兩種寫法一樣
            _httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var token2);
            mytoken = token2.FirstOrDefault() ?? ""; // request header的值存在 StringValues 物件中，以便處理多個值的情況
            Console.WriteLine($"1. - {mytoken}");

            string? Access_Token_string = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"];
            Console.WriteLine($"1. - {Access_Token_string}");
            #endregion

            #region Redis獲取資訊
            // RedisClient.Init("localhost:6379");

            #endregion

            string a = "";
            return (a, a, a, a);
        }
    }
}