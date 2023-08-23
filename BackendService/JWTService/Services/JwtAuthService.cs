using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

    public AuthResult CreateJWT(string issuer)
    {
        try
        {
            #region 建立JWT Token
            //appsettings中JwtConfig的Secret值
            byte[] key = Encoding.ASCII.GetBytes(_jwtconfig.SignKey);

            //定義token描述
            //SecurityTokenDescriptor下設定的是回傳的payload內容，而Subject設定的不知要幹嘛，別的範例是用來接收client端自定義的payload內容
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = "iRent發行", // 設置發行者資訊
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Iss, issuer),
                    new Claim(JwtRegisteredClaimNames.Email, "xxx@gmail.com"),
                    new Claim(JwtRegisteredClaimNames.Nbf, "1000")
                }),
                Expires = DateTime.Now.AddSeconds(300), //設定Token的時效
                // NotBefore = DateTime.Now,

                //設定加密方式，key(appsettings中JwtConfig的Secret值)與HMAC SHA512演算法
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            JwtSecurityTokenHandler jwtTokenHandler = new();//宣告JwtSecurityTokenHandler，用來建立token
            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);//使用SecurityTokenDescriptor建立JWT securityToken
            string jwtToken = jwtTokenHandler.WriteToken(token);//token序列化為字串
            #endregion

            //寫入資料庫
            Console.WriteLine($"Account:{issuer}");
            Console.WriteLine($"Token:{jwtToken}");
            Console.WriteLine($"RefreshToken:");

            return new AuthResult()
            {
                JwtToken = jwtToken,
                Result = true,
                RefreshToken = ""
            };
        }
        catch (Exception ex)
        {
            return new AuthResult()
            {
                JwtToken = ex.ToString(),
                Result = false,
                RefreshToken = "你他媽沒拿到token啦"
            };
        }
    }
    /// <summary>
    /// 驗證Token，並重新產生Token
    /// </summary>
    public bool VerifyAndGenerateToken(TokenRequest tokenRequest)
    {
        //建立JwtSecurityTokenHandler
        JwtSecurityTokenHandler jwtTokenHandler = new();

        try
        {
            //驗證參數的Token，回傳SecurityToken
            ClaimsPrincipal tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.JwtToken, _tokenValidationParams, out SecurityToken validatedToken);

            //取Token Claims中的Iss(產生token時定義為Account)
            Console.WriteLine(tokenInVerification.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Iss).Value);

            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                //檢核Token的演算法
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);

                if (result == false)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}