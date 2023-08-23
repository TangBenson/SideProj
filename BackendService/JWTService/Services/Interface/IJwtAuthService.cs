using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTService.Models;

namespace JWTService.Services.Interface
{
    public interface IJwtAuthService
    {
        AuthResult CreateJWT(
            string issuer
        );
        bool VerifyAndGenerateToken(TokenRequest tokenRequest);
    }
}