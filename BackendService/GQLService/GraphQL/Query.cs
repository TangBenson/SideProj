using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GQLService.Models;
using HotChocolate;
using HotChocolate.Data;

namespace GQLService.GraphQL;
public class Query
{
    [GraphQLDescription("GetTest")]
    // [UseSorting]
    // [UseFiltering]
    public IQueryable<string> GetTest() => new List<string> { "Hello", "World" }.AsQueryable();

    public string GetTest2() => "Hello World";

    public IQueryable<LoginOutput> Login(LoginInput input){
        return new List<LoginOutput> { new LoginOutput() }.AsQueryable();
    }

    public IQueryable<Car> GetCars()
    {
        
        return new List<Car> { 
            new Car() { CarNo = "AAA-1234", Lat = 25.123, Lng = 121.123, Price = 1000, CID = "c1", Type = "yaris", Brand = "Toyota", Type2 = "跑車" },
            new Car() { CarNo = "BBB-1234", Lat = 25.123, Lng = 121.123, Price = 1000, CID = "c2", Type = "city", Brand = "Honda", Type2 = "跑車" },
            new Car() { CarNo = "CCC-1234", Lat = 25.123, Lng = 121.123, Price = 1000, CID = "c3", Type = "sentra", Brand = "Nissan", Type2 = "跑車" },
            new Car() { CarNo = "DDD-1234", Lat = 25.123, Lng = 121.123, Price = 1000, CID = "c4", Type = "golf", Brand = "VW", Type2 = "跑車" },
            }.AsQueryable();
    }
}