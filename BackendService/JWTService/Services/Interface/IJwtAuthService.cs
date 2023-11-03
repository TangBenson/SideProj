using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTService.Models;

namespace JWTService.Services.Interface
{
    public interface IJwtAuthService
    {
        AuthResult GenerateJwtToken(
            string issuer,
            string mail
        );
        AuthResult VerifyAndGenerateToken(TokenVerify tokenRequest);
    }
}