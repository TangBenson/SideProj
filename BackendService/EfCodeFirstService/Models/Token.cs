using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCodeFirstService.Models;

public class Token
{
    public string ID { get; set; } = "";
    public string AccessToken { get; set; } = "";
    public string RefreshTokeno { get; set; } = "";
    public DateTime ExpireTime { get; set; }
}
