using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MemDataService.DbConnect;
using MemDataService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public GetDataService(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public List<MortorData> GetUserData()
        {
            #region 驗證 JwtToken並轉成 IDNO
            //兩種寫法一樣
            // _httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var token2);
            // mytoken = token2.FirstOrDefault() ?? ""; // request header的值存在 StringValues 物件中，以便處理多個值的情況
            // Console.WriteLine($"1. - {mytoken}");

            // string? Access_Token_string = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"];
            // Console.WriteLine($"1. - {Access_Token_string}");
            #endregion

            #region Redis獲取資訊
            // RedisClient.Init("localhost:6379");
            // var redisDb = RedisClient.Database;
            // memdata = redisDb.StringGet("B123456789");
            // if (!memdata.IsNullOrEmpty())
            // {
            //     Console.WriteLine(1);
            //     MemberData? data = System.Text.Json.JsonSerializer.Deserialize<MemberData>(memdata!);
            //     return data;
            // }
            // else
            // {
            //     Console.WriteLine(2);
            //     List<MemberData> a = _context.member.AsNoTracking().Where(x => x.ID == "B123456789").ToList();
            //     redisDb.StringSet("B123456789", System.Text.Json.JsonSerializer.Serialize(a.FirstOrDefault()));
            //     return a.FirstOrDefault();
            // }
            #endregion

            #region
            RedisClient.Init("localhost:6379");
            var redisDb = RedisClient.Database;

            memdata = redisDb.StringGet("motorlist");
            List<MortorData> data = System.Text.Json.JsonSerializer.Deserialize<List<MortorData>>(memdata!);
            return data;
            
            // var parameters = new[]
            // {
            //     new SqlParameter("@IDNO", SqlDbType.VarChar) { Value = "" },
            //     new SqlParameter("@LAT", SqlDbType.VarChar) { Value = "25.052338" },
            //     new SqlParameter("@LON", SqlDbType.VarChar) { Value = "121.526446" },
            //     new SqlParameter("@Radius", SqlDbType.Float) { Value = 3000.0 },
            //     new SqlParameter("@ErrorCode", SqlDbType.VarChar, 6) { Direction = ParameterDirection.Output },
            //     new SqlParameter("@ErrorMsg", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output },
            //     new SqlParameter("@SQLExceptionCode", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output },
            //     new SqlParameter("@SQLExceptionMsg", SqlDbType.VarChar, 1000) { Direction = ParameterDirection.Output }
            // };
            // var results = _context.mortor.FromSqlRaw(@"EXEC usp_GetMotorRent_Test @IDNO, 0, @LAT, @LON, @Radius, 180170
            //             , @ErrorCode OUTPUT, @ErrorMsg OUTPUT, @SQLExceptionCode OUTPUT, @SQLExceptionMsg OUTPUT", parameters).ToList();
            // redisDb.StringSet("motorlist", System.Text.Json.JsonSerializer.Serialize(results));

            // memdata = redisDb.StringGet("motorlist");
            // List<MortorData> data = System.Text.Json.JsonSerializer.Deserialize<List<MortorData>>(memdata!);
            // return data;

            #endregion
        }
    }
}