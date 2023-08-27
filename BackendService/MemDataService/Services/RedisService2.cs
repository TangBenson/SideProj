using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MemDataService.Services
{
    public class RedisService2
    {
        private static readonly Lazy<ConnectionMultiplexer> s_connectionLazy;
        private static string _setting;

        private ConnectionMultiplexer Instance => s_connectionLazy.Value;

        public IDatabase Database => this.Instance.GetDatabase();

        static RedisService2()
        {
            s_connectionLazy = new Lazy<ConnectionMultiplexer>(() =>
            {
                if (string.IsNullOrWhiteSpace(_setting))
                {
                    return ConnectionMultiplexer.Connect("localhost");
                }

                return ConnectionMultiplexer.Connect(_setting);
            });
        }

        public static void Init(string setting)
        {
            _setting = setting;
        }
    }
}