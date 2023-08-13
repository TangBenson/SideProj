using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTService.Models;
using JWTService.Services.Interface;
using Microsoft.IdentityModel.Tokens;

namespace JWTService.Services;

public class JwtAuthService : IJwtAuthService
{
    private readonly IConfiguration _config;
    public JwtAuthService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public AuthResult CreateJWT(JWTCliam jWTCliam, string issuer, int expireMinutes = 30)
    {
        AuthResult response = new AuthResult();
        try
        {
            #region Step 1. 取得資訊聲明(claims)集合
            List<Claim> claims = GenCliams(jWTCliam ?? new JWTCliam());
            #endregion

            #region  Step 2. 建置資訊聲明(claims)物件實體，依據上面步驟產生Data來做
            ClaimsIdentity userClaimsIdentity = new ClaimsIdentity(claims);
            #endregion

            #region Step 3. 建立Token加密用金鑰
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings")["SignKey"]!));
            #endregion

            #region Step 4. 建立簽章，依據金鑰
            // 使用HmacSha256進行加密
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            #endregion

            #region  Step 5. 建立Token內容實體
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                // Issuer = issuer, // 設置發行者資訊
                // Audience = issuer, // 設置驗證發行者對象，如果需要驗證Token發行者，需要設定此項目
                // NotBefore = DateTime.Now, // 設置可用時間， 預設值就是 DateTime.Now
                // IssuedAt = DateTime.Now, // 設置發行時間，預設值就是 DateTime.Now
                Subject = userClaimsIdentity, // Token 針對User資訊內容物件
                Expires = DateTime.Now.AddMinutes(expireMinutes), // 建立Token有效期限
                SigningCredentials = signingCredentials // Token簽章
            };
            #endregion

            #region Step 6. 產生JWT Token並轉換成字串
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler(); // 建立一個JWT Token處理容器
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);  // 將Token內容實體放入JWT Token處理容器
            string serializeToken = tokenHandler.WriteToken(securityToken); // 最後將JWT Token處理容器序列化，這一個就是最後會需要的Token 字串
            #endregion

            response.result = true; // 告訴使用此請求一方Token成功產生
            response.jwt = serializeToken; // 放置產生的Token字串
            response.msg = "ok";
        }
        catch (Exception ex)
        {
            response.result = false;
            response.jwt = string.Empty;
            if (_config["debug"] == "t")
                response.msg = ex.ToString();
            else
                response.msg = "Token產出發生錯誤.";

        }

        return response;
    }

    /// <summary>
    /// 建置資訊聲明集合
    /// </summary>
    /// <param name="jWTCliam">Token資訊聲明內容物件</param>
    /// <returns>一組收集資訊聲明集合</returns>
    private List<Claim> GenCliams(JWTCliam jWTCliam)
    {
        List<Claim> claims = new List<Claim>();

        // (audience),設定Token接受者，用在驗證接收者驗證是否相符
        // if (jWTCliam.aud != null && jWTCliam.aud != string.Empty)
        // {
        //     claims.Add(new Claim(JwtRegisteredClaimNames.Aud, jWTCliam.aud));
        // }
        claims.Add(new Claim(JwtRegisteredClaimNames.Aud, string.IsNullOrEmpty(jWTCliam.aud) ? "" : jWTCliam.aud));

        // (expiration time),Token過期時間，一但超過這時間此Token就失效
        claims.Add(new Claim(JwtRegisteredClaimNames.Exp, string.IsNullOrEmpty(jWTCliam.exp) ? "" : jWTCliam.exp));

        // (issued at time),Token發行時間，用在後面檢查Token發行多久
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, string.IsNullOrEmpty(jWTCliam.iat) ? "" : jWTCliam.iat));

        // (issuer),發行者資訊
        claims.Add(new Claim(JwtRegisteredClaimNames.Iss, string.IsNullOrEmpty(jWTCliam.iss) ? "" : jWTCliam.iss));

        // (JWT ID),Token ID，避免Token重複在被套用
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, string.IsNullOrEmpty(jWTCliam.jti) ? "" : jWTCliam.jti));

        // (not before time),Token有效起始時間，用來驗證Token可用時間
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, string.IsNullOrEmpty(jWTCliam.nbf) ? "" : jWTCliam.nbf));

        // (subject),Token 主題，放置該User內容
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, string.IsNullOrEmpty(jWTCliam.sub) ? "" : jWTCliam.sub));

        // 添加自定義的聲明
        claims.Add(new Claim("custName", jWTCliam.custName));
        claims.Add(new Claim("custMode", jWTCliam.custMode));


        return claims;
    }


    // private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
    // {
    //     //建立JwtSecurityTokenHandler
    //     JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();

    //     try
    //     {
    //         //驗證參數的Token，回傳SecurityToken
    //         ClaimsPrincipal tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out SecurityToken validatedToken);

    //         if (validatedToken is JwtSecurityToken jwtSecurityToken)
    //         {
    //             //檢核Token的演算法
    //             var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);

    //             if (result == false)
    //             {
    //                 return null;
    //             }
    //         }

    //         //依參數的RefreshToken，查詢UserToken資料表中的資料
    //         UserToken storedRefreshToken = _context.UserTokens.Where(x => x.RefreshToken == tokenRequest.RefreshToken).FirstOrDefault();

    //         if (storedRefreshToken == null)
    //         {
    //             return new AuthResult()
    //             {
    //                 msg = "Refresh Token不存在",
    //                 result = false
    //             };
    //         }

    //         //取Token Claims中的Iss(產生token時定義為Account)
    //         string JwtAccount = tokenInVerification.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Iss).Value;

    //         //檢核storedRefreshToken與JwtAccount的Account是否一致
    //         if (storedRefreshToken.Account != JwtAccount)
    //         {
    //             return new AuthResult()
    //             {
    //                 msg = "Token驗證失敗",
    //                 result = false
    //             };
    //         }

    //         //依storedRefreshToken的Account，查詢出DB的User資料
    //         User dbUser = _context.Users.Where(u => u.Account == storedRefreshToken.Account).FirstOrDefault();

    //         //產生Jwt Token
    //         return await CreateJWT(dbUser);
    //     }
    //     catch (Exception ex)
    //     {
    //         return new AuthResult()
    //         {
    //             result = false,
    //             msg =ex.Message
    //         };
    //     }
    // }
}