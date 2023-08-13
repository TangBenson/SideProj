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
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        private JwtAuthService2 _a;
        private readonly JWTConfig _jwtconfig;
        private IJwtAuthService _jwtAuthService;

        public JWTController(
            IConfiguration configuration,
            IHttpContextAccessor httpContext,
            IOptionsMonitor<JWTConfig> option,
            IJwtAuthService jwtAuthService,
            JwtAuthService2 aa)
        {
            _config = configuration;
            _httpContext = httpContext;
            // 昨天很困擾我的controller和service都要注入iconfig和ihttp的問題，我只要在controller簽章中多帶入JwtAuthService2 aa並賦值給_a即可
            // _a = new JwtAuthService2(_config, _httpContext);
            _a = aa;
            _jwtconfig = option.CurrentValue;
            _jwtAuthService = jwtAuthService;
        }

        // [HttpGet]
        // // [AllowAnonymous] //允許未經驗證的使用者存取個別動作
        // // [Authorize] //限制呼叫時須透過驗證機制
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

        // [HttpGet(Name = "GetToken")]
        // public AuthResult Get2()
        // {
        //     return _jwtAuthService.CreateJWT(null,"Benson",0);
        // }

        // //測試是否通過驗證
        // [HttpPost]
        // public bool IsAuthenticated()
        // {
        //     var user = _a.Validate();
        //     if (user == null)
        //     {
        //         return false;
        //     }
        //     return true;
        // }

        [HttpGet]
        public Token Get(string user)
        {
            return _a.Create(user);
        }

    }
}