using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MemDataService.Services;
// sealed修飾class表示無法被繼承，修飾method表示無法被子類覆寫
public class RedisConnection
{
    private static Lazy<RedisConnection> lazy = new Lazy<RedisConnection>(() =>
    {
        if (String.IsNullOrEmpty(_settingOption)) throw new InvalidOperationException("Please call Init() first.");
        return new RedisConnection();
    });
    private static string _settingOption;






    public static RedisConnection Instance =>lazy.Value;
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