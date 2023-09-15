using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCodeFirstService.DbConnect;
using EfCodeFirstService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EfCodeFirstService.Controllers;

[ApiController]
[Route("api/[controller]")] //http://localhost:5046/api/InserData
public class InserDataController : ControllerBase
{
    private readonly AppDbContext _context;
    public InserDataController(AppDbContext context)
    {
        _context = context;
    }
    public async Task PushAsync()
    {
        // var cardata = new Car()
        // {
        //     CarNo = "AAA-1110",
        //     Lat = 25.0559612814796,
        //     Lon = 121.54713301287657,
        //     Status = 0,
        // };
        // _context.car.Add(cardata);

        var cardatalist = new List<Car>()
        {
            new Car{
                CarNo = "ACG-1111",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "ACG-1112",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "ACG-1113",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "ACG-1114",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
            new Car{
                CarNo = "ACG-1115",
                Lat = 25.0559612814796,
                Lon = 121.54713301287657,
                Status = 0
            },
        };
        _context.car.AddRange(cardatalist);
        await _context.SaveChangesAsync();


        var mematalist = new List<MemberData>()
        {
            new MemberData{
                ID = "A123456789",
                Basvd = "",
                Name = "老大",
                Email = "",
                Phone=""
            },new MemberData{
                ID = "B123456789",
                Basvd = "",
                Name = "老二",
                Email = "",
                Phone=""
            },new MemberData{
                ID = "C123456789",
                Basvd = "",
                Name = "老三",
                Email = "",
                Phone=""
            },new MemberData{
                ID = "D123456789",
                Basvd = "",
                Name = "老四",
                Email = "",
                Phone=""
            },new MemberData{
                ID = "E123456789",
                Basvd = "",
                Name = "老五",
                Email = "",
                Phone=""
            }
        };
        _context.member.AddRange(mematalist);
        await _context.SaveChangesAsync();
    }
}
