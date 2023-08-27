using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemDataService.Services
{
    public interface IRedisService
    {
        RedisService Instance { get; }
        void Init(string settingOption);
    }
}