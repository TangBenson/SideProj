// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using StackExchange.Redis;

// namespace MemDataService.Services;
// public sealed class RedisClient
// {
//     // 宣告一個 Lazy物件用於延遲初始化 ConnectionMultiplexer物件
//     private static readonly Lazy<ConnectionMultiplexer> s_connectionLazy;




//     private static string _setting = "";
//     /*
//     這是一個 C# 8.0 引入的簡化屬性（Simplified Property）的寫法，
//     等同 private ConnectionMultiplexer Instance{ get{ return s_connectionLazy.Value; }}。
//     而 private ConnectionMultiplexer Instance = s_connectionLazy.Value;
//     這是一個字段（field）的定義，它的值在初始化後不會再改變。這就是字段和屬性之間的區別。
//     */
//     // 通過 s_connectionLazy.Value取得 ConnectionMultiplexer實例
//     public static ConnectionMultiplexer Instance => s_connectionLazy.Value;
//     public static IDatabase Database => Instance.GetDatabase();
//     static RedisClient()
//     {
//         /*
//         初始化了 s_connectionLazy 对象。s_connectionLazy 的初始化是通过一个 Lazy 委托来进行的，
//         该委托会在首次访问 Instance 属性时执行
//         */
//         s_connectionLazy = new Lazy<ConnectionMultiplexer>(() =>
//         {
//             if (string.IsNullOrWhiteSpace(_setting))
//             {
//                 return ConnectionMultiplexer.Connect("localhost");
//             }
//             return ConnectionMultiplexer.Connect(_setting);
//         });
//     }
//     public static void Init(string setting)
//     {
//         _setting = setting;
//     }
// }