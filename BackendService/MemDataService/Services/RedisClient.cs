using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MemDataService.Services;
public class RedisClient
{
    private static readonly Lazy<ConnectionMultiplexer> s_connectionLazy;
    private static string _setting;
    /*
    這是一個 C# 8.0 引入的簡化屬性（Simplified Property）的寫法，
    等同 private ConnectionMultiplexer Instance{ get{ return s_connectionLazy.Value; }}。
    而 private ConnectionMultiplexer Instance = s_connectionLazy.Value;
    這是一個字段（field）的定義，它的值在初始化後不會再改變。這就是字段和屬性之間的區別。
    */
    public ConnectionMultiplexer Instance => s_connectionLazy.Value;
    public IDatabase Database => this.Instance.GetDatabase();
    static RedisClient()
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