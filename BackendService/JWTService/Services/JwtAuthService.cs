using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTService.Models;
using JWTService.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTService.Services;

public class JwtAuthService : IJwtAuthService
{
    private readonly JWTConfig _jwtconfig;
    private readonly TokenValidationParameters _tokenValidationParams;
    public JwtAuthService(IOptionsMonitor<JWTConfig> option, TokenValidationParameters tokenValidationParams)
    {
        // 發現有些人不喜歡注入iconfig，而是用這種方式
        _jwtconfig = option.CurrentValue;
        _tokenValidationParams = tokenValidationParams;
    }

    public AuthResult GenerateJwtToken(string issuer,string mail)
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

            UserToken userToken = new()
            {
                Account = issuer,
                AccessToken = jwtToken,
                RefreshToken = DateTime.Now.ToString(),
                ExpireTime = exp
            };

            //寫入資料庫
            Console.WriteLine($"Account:{userToken.Account}");
            Console.WriteLine($"Token:{userToken.AccessToken}");
            Console.WriteLine($"RefreshToken:{userToken.RefreshToken}");

            return new AuthResult()
            {
                AccessToken = userToken.AccessToken,
                Result = true,
                RefreshToken = userToken.RefreshToken
            };
        }
        catch (Exception ex)
        {
            return new AuthResult()
            {
                AccessToken = ex.ToString(),
                Result = false,
                RefreshToken = "你他媽沒拿到token啦"
            };
        }
    }
    /// <summary>
    /// 驗證Token，並重新產生Token
    /// </summary>
    public AuthResult VerifyAndGenerateToken(TokenRequest tokenRequest)
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
            UserToken storedRefreshToken = _context.UserTokens.Where(x => x.RefreshToken == tokenRequest.RefreshToken).FirstOrDefault();

            if (storedRefreshToken == null)
            {
                return new AuthResult()
                {
                    RefreshToken = "Refresh Token不存在",
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
                    RefreshToken = "Token驗證失敗" ,
                    Result = false
                };
            }

            //依storedRefreshToken的Account，查詢出DB的User資料
            string account = _context.Users.Where(u => u.Account == storedRefreshToken.Account).FirstOrDefault();
            string mail = _context.Users.Where(u => u.Account == storedRefreshToken.Account).FirstOrDefault();

            //產生Jwt Token
            return GenerateJwtToken(account,mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new AuthResult
            {
                Result = false,
                AccessToken = "",
                RefreshToken = ""
            };
        }
    }
}