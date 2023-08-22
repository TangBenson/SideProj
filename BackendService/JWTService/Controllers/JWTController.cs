using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTService.Models;
using JWTService.Services;
using JWTService.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JWTService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class JWTController : ControllerBase
    {
        // private readonly JWTConfig _jwtconfig; //測試一些取值,與jwt無關
        // private readonly JwtAuthService2 _jwt2;
        private readonly IJwtAuthService _jwt;

        public JWTController(
            // IOptionsMonitor<JWTConfig> option,
            // JwtAuthService2 jwt2
            IJwtAuthService jwt
            )
        {
            // _jwtconfig = option.CurrentValue;

            // 昨天很困擾我的controller和service都要注入iconfig和ihttp的問題,我只要在controller簽章中多帶入JwtAuthService2 jwt2並賦值給_jwt2即可
            // _jwt2 = new JwtAuthService2(_config, _httpContext);
            // _jwt2 = jwt2;

            _jwt = jwt;
        }

        #region 測試一些取值,與jwt無關
        // [HttpGet]
        // public ActionResult Get()
        // {
        //     try
        //     {
        //         return Ok($"操爆綺綺的小穴 - {_jwtconfig.Issuer}");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //     }
        // }
        #endregion

        #region JwtAuthService2用的,這個範例service是手刻整套jwt流程,包含編碼+雜湊,又多加aes加密
        // //測試是否通過驗證
        // [HttpPost]
        // // [AllowAnonymous] //允許未經驗證的使用者存取個別動作
        // // [Authorize] //限制呼叫時須透過驗證機制,如果沒有通過權限校驗,則http返回狀態碼爲401
        // public bool IsAuthenticated()
        // {
        //     var user = _jwt2.Validate();
        //     if (user == null)
        //     {
        //         return false;
        //     }
        //     return true;
        // }

        // [HttpGet]
        // public Token Get(string user)
        // {
        //     return _jwt2.Create(user);
        // }
        #endregion
        
        #region JwtAuthService用的
        [HttpGet(Name = "GetToken")]
        public AuthResult Get2()
        {
            return _jwt.CreateJWT("Benson");
        }
        #endregion
    }
}