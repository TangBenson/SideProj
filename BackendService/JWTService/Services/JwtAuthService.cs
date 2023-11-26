using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EFCoreService.DbConnect;
using EFCoreService.Models;
using JWTService.Models;
using JWTService.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTService.Services;

public class JwtAuthService : IJwtAuthService
{
    private readonly JWTConfig _jwtconfig;
    private readonly TokenValidationParameters _tokenValidationParams;
    private readonly AppDbContext _context;
    public JwtAuthService(IOptionsMonitor<JWTConfig> option, TokenValidationParameters tokenValidationParams, AppDbContext context)
    {
        // 發現有些人不喜歡注入iconfig，而是用這種方式
        _jwtconfig = option.CurrentValue;
        _tokenValidationParams = tokenValidationParams;
        _context = context;
    }

    public async Task<AuthResult> GenerateJwtToken(string issuer, string mail)
    {
        try
        {
            #region 建立JWT Token
            //appsettings中JwtConfig的Secret值
            byte[] key = Encoding.ASCII.GetBytes(_jwtconfig.SignKey);
            DateTime exp = DateTime.UtcNow.AddSeconds(3000);

            //定義token描述，SecurityTokenDescriptor下設定的是回傳的payload內容，Subject用來接收client端自定義的payload內容，若重覆會抓Subject外層的
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                // Nbf 預設此時此刻
                // Iat是Token簽發時間，預設此時此刻
                Issuer = _jwtconfig.Issuer, // 設置發行者資訊
                Audience = issuer,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Iss, issuer),
                    new Claim(JwtRegisteredClaimNames.Email, mail),
                }),
                Expires = exp, //設定Token的時效
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature) //設定加密方式
            };
            JwtSecurityTokenHandler jwtTokenHandler = new();//宣告JwtSecurityTokenHandler，用來建立token
            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);//使用SecurityTokenDescriptor建立JWT securityToken
            string jwtToken = jwtTokenHandler.WriteToken(token);//token序列化為字串
            #endregion

            Token userToken = new()
            {
                Account = issuer,
                AccessToken = jwtToken,
                RefreshTokeno = DateTime.Now.ToString(),
                ExpireTime = exp
            };

            //寫入資料庫
            if (_context.Jwttoken.Where(x => x.Account == issuer).Any())
            {
                _context.Jwttoken.Update(userToken);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Jwttoken.Add(userToken);
                await _context.SaveChangesAsync();
            }
            // Console.WriteLine($"Account:{userToken.Account}");
            // Console.WriteLine($"Token:{userToken.AccessToken}");
            // Console.WriteLine($"RefreshToken:{userToken.RefreshTokeno}");
            Console.WriteLine("a");

            return new AuthResult()
            {
                AccessToken = userToken.AccessToken,
                Result = true,
                RefreshTokeno = userToken.RefreshTokeno
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("a");
            return new AuthResult()
            {
                AccessToken = ex.ToString(),
                Result = false,
                RefreshTokeno = "你他媽沒拿到token啦"
            };
        }
    }
    /// <summary>
    /// 驗證Token，並重新產生Token
    /// </summary>
    public async Task<AuthResult> VerifyAndGenerateToken(TokenVerify tokenRequest)
    {
        //建立JwtSecurityTokenHandler
        JwtSecurityTokenHandler jwtTokenHandler = new();

        try
        {
            //驗證參數的Token，回傳SecurityToken
            ClaimsPrincipal tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.AccessToken, _tokenValidationParams, out SecurityToken validatedToken);

            //取Token Claims中的Iss(產生token時定義為Account)
            Console.WriteLine(tokenInVerification.Claims?.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Iss)!.Value);

            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                //檢核Token的演算法
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);

                if (result == false)
                    return null!;
            }

            //依參數的RefreshToken，查詢UserToken資料表中的資料
            Token storedRefreshToken = _context.Jwttoken.Where(x => x.RefreshTokeno == tokenRequest.RefreshTokeno).FirstOrDefault()!;

            if (storedRefreshToken == null)
            {
                return new AuthResult()
                {
                    RefreshTokeno = "Refresh Token不存在",
                    Result = false
                };
            }

            //取Token Claims中的Iss(產生token時定義為Account)
            string JwtAccount = tokenInVerification.Claims!.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Iss)!.Value;

            //檢核storedRefreshToken與JwtAccount的Account是否一致
            if (storedRefreshToken.Account != JwtAccount)
            {
                return new AuthResult()
                {
                    RefreshTokeno = "Token驗證失敗",
                    Result = false
                };
            }

            //依storedRefreshToken的Account，查詢出DB的User資料
            string account = _context.Member.Where(u => u.ID == storedRefreshToken.Account).Select(u => u.ID).FirstOrDefault();
            string mail = _context.Member.Where(u => u.ID == storedRefreshToken.Account).Select(u => u.Email).FirstOrDefault();

            //產生Jwt Token
            return await GenerateJwtToken(account, mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new AuthResult
            {
                Result = false,
                AccessToken = "",
                RefreshTokeno = ""
            };
        }
    }
}