using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTService.Models;

namespace JWTService.Services.Interface
{
    public interface IJwtAuthService
    {
        Task<AuthResult> GenerateJwtToken(
            string issuer,
            string mail
        );
        Task<AuthResult> VerifyAndGenerateToken(TokenVerify tokenRequest);
    }
}