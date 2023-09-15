// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MemDataService.Services;
// using Microsoft.AspNetCore.Mvc;
// // using StackExchange.Redis;

// namespace MemDataService.Controllers;

// [ApiController]
// [Route("api/[controller]/[action]")]
// public class RedisController : ControllerBase
// {
//     public RedisController()
//     {
//     }

//     [HttpGet]
//     public IActionResult Get2()
//     {
//         // 初始化 Redis 連接
//         RedisClient.Init("localhost:6379"); // 可以傳入 Redis 伺服器的連接字串

//         // 寫入資料到 Redis
//         var redisDb = RedisClient.Database;
//         redisDb.StringSet("myKey", "Hello, Redis!");

//         // 從 Redis 讀取資料
//         string result = redisDb.StringGet("myKey");

//         Console.WriteLine($"Value from Redis: {result}");

//         //Redis的PUB/SUB的訊息機制小玩，相比RabbitMQ陽春很多
//         // var sub = RedisClient.Instance.GetSubscriber();
//         // Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss.fff} - Subscribed channel topic.test ");
//         // sub.Subscribe(RedisChannel.Literal("topic.test"), (channel, message) =>
//         // {
//         //     Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss.fff} - Received message {message}");
//         // });
//         // RedisClient.Instance.GetDatabase().Publish(RedisChannel.Literal("topic.test"), "Hello World!");
//         // Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss.fff} - Published message to channel topic.test");

//         RedisClient.Instance.Close();

//         return Ok("成功");
//     }

//     [HttpGet]
//     public IActionResult Get3()
//     {
//         A a1 = new();
//         A a2 = new();
//         Console.WriteLine(a1 == a2);

//         var redis = ConnectionMultiplexer.Connect("localhost:6379");
//         var db = redis.GetDatabase();
//         db.StringSet("foo", 1688);
//         var result = db.StringGet("foo");
//         Console.WriteLine(result);
//         redis.Dispose();

//         return Ok("成功");
//     }

//     public IActionResult Get4()
//     {
//         RedisConnection.Init("localhost:6379");
//         var redis = RedisConnection.Instance.ConnectionMultiplexer;
//         var db = redis.GetDatabase(0);
//         db.StringSet("Peggy", 160);
//         var result = db.StringGet("Peggy");
//         Console.WriteLine(result);

//         //Redis的PUB/SUB的訊息機制小玩，相比RabbitMQ陽春很多
//         // var sub = redis.GetSubscriber();
//         // Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss.fff} - Subscribed channel topic.test ");
//         // sub.Subscribe(RedisChannel.Literal("topic.test"), (channel, message) =>
//         // {
//         //     Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss.fff} - Received message {message}");
//         // });
//         // redis.GetDatabase().Publish(RedisChannel.Literal("topic.test"), "Hello World!");
//         // Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss.fff} - Published message to channel topic.test");

//         redis.Dispose();

//         return Ok("成功");
//     }
// }

// public class A
// {
//     public int a;
// };