using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MemDataService.Services;

// sealed修飾class表示無法被繼承，修飾method表示無法被子類覆寫
// 原範例的Init和Instance都宣告成static，但由於介面不能有static，故我拿掉
public sealed class RedisService : IRedisService
{
    private static Lazy<RedisService> lazy = new Lazy<RedisService>(() =>
        {
            if (String.IsNullOrEmpty(_settingOption)) throw new InvalidOperationException("Please call Init() first.");
            return new RedisService();
        });
    private static string _settingOption;
    public readonly ConnectionMultiplexer connectionMultiplexer;
    public RedisService Instance { get { return lazy.Value; } }
    public RedisService()
    {
        connectionMultiplexer = ConnectionMultiplexer.Connect(_settingOption);
    }
    public void Init(string settingOption)
    {
        _settingOption = settingOption;
    }
}