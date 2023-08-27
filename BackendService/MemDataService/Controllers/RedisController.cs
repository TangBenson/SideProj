using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemDataService.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace MemDataService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RedisController : ControllerBase
{
    private readonly IRedisService _redis;
    public RedisController(IRedisService redis)
    {
        _redis = redis;
    }

    [HttpGet]
    public void Get()
    {
        _redis.Init("localhost:6379");
        var redis = _redis.Instance.connectionMultiplexer;
        var db = redis.GetDatabase(0);
        Console.WriteLine("Hello World!");
        Console.Read();
    }
    // [HttpGet]
    // public string Get2()
    // {
    //     _redis.Init("localhost:6379");
    //     var redis = _redis.Instance.ConnectionMultiplexer;
    //     var db = redis.GetDatabase(0);
    //     return "e";
    // }
}
