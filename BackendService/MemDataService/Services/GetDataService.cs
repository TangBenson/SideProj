using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EFCoreService.DbConnect;
using MemDataService.Models;
using Microsoft.IdentityModel.Tokens;
using RedisService;

namespace MemDataService.Services
{
    public class GetDataService : IGetDataService
    {
        private string mytoken = "";
        private string? memdata = "";
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenValidationParameters _tokenValidationParams;
        public GetDataService(
            IHttpContextAccessor httpContextAccessor,
            AppDbContext context,
            TokenValidationParameters tokenValidationParams)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _tokenValidationParams = tokenValidationParams;
        }
        public MemberData GetUserData()
        {
            #region 驗證 JwtToken並轉成 IDNO
            //兩種寫法一樣
            _httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var token);
            mytoken = token.FirstOrDefault() ?? ""; // request header的值存在 StringValues 物件中，以便處理多個值的情況
            Console.WriteLine($"1. - {mytoken}");

            string? Access_Token_string = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"];
            Console.WriteLine($"1. - {Access_Token_string}");

            //建立JwtSecurityTokenHandler
            JwtSecurityTokenHandler jwtTokenHandler = new();
            
            //驗證參數的Token，回傳SecurityToken
            ClaimsPrincipal tokenInVerification = jwtTokenHandler.ValidateToken(
                Access_Token_string,
                _tokenValidationParams,
                out SecurityToken validatedToken);

            //取Token Claims中的Iss(產生token時定義為Account)
            string issuer = tokenInVerification.Claims?.SingleOrDefault(
                x => x.Type == JwtRegisteredClaimNames.Iss)!.Value!;
            #endregion

            #region Redis獲取資訊
            RedisClient.Init("localhost:6379");
            var redisDb = RedisClient.Database;
            memdata = redisDb.StringGet("memdata");
            if (string.IsNullOrEmpty(memdata))
            {
                Console.WriteLine("Redis沒有資料");
                List<MemberData> a = _context.Member.Where(x => x.ID == issuer).ToList();
                redisDb.StringSet("B123456789", System.Text.Json.JsonSerializer.Serialize(a.FirstOrDefault()));
                return a.FirstOrDefault()!;
            }
            else
            {
                Console.WriteLine("Redis有資料");
                List<MemberData> data = System.Text.Json.JsonSerializer.Deserialize<List<MemberData>>(memdata!)!;
                return data.FirstOrDefault()!;
            }
            #endregion
        }
    }
}