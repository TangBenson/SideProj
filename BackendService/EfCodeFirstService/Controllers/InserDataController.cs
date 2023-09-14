using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCodeFirstService.DbConnect;
using EFcoreService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EfCodeFirstService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InserDataController : ControllerBase
{
    private AppDbContext _context;
    public InserDataController(AppDbContext context)
    {
        _context = context;
    }
    public async Task PushAsync()
    {
        // var cardata = new Car()
        // {
        //     CarNo = "AAA-1111",
        //     Lat = 25.0559612814796,
        //     Lon = 121.54713301287657,
        //     Status = 0,
        // };
        // _context.car.Add(cardata);

        var cardatalist = new List<Car>()
        {
            new Car{
                CarNo = "AAA-1110",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "AAA-1112",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "AAA-1113",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "AAA-1114",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "AAA-1115",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "AAA-1116",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            }
        };
        _context.car.AddRange(cardatalist);
        await _context.SaveChangesAsync();
    }
}
