using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GQLService.Models
{
    public class LoginOutput
    {
        public string? Result { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}