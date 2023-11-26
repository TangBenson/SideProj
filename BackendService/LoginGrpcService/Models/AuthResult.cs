using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginGrpcService.Models
{
    public class AuthResult
    {
        [System.Text.Json.Serialization.JsonPropertyName("result")]
        public bool Result { set; get; } = false;
        [System.Text.Json.Serialization.JsonPropertyName("accessToken")]
        public string AccessToken { set; get; } = "";
        [System.Text.Json.Serialization.JsonPropertyName("refreshTokeno")]
        public string RefreshTokeno { set; get; } = "";
    }
}