using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisService;
/*
這寫法看不懂，我還是以RedisClient為主
*/
public class RedisConnection
{
    private static Lazy<RedisConnection> lazy = new Lazy<RedisConnection>(() =>
    {
        if (String.IsNullOrEmpty(_settingOption)) throw new InvalidOperationException("Please call Init() first.");
        return new RedisConnection();
    });
    private static string _settingOption;

    public static RedisConnection Instance => lazy.Value;
    public readonly ConnectionMultiplexer ConnectionMultiplexer;
    private RedisConnection()
    {
        ConnectionMultiplexer = ConnectionMultiplexer.Connect(_settingOption);
    }

    public static void Init(string settingOption)
    {
        _settingOption = settingOption;
    }
}