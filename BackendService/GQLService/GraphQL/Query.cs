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

    public LoginOutput Login(LoginInput input)
    {
        // query{
        //     login(
        //         input:{
        //             account:"csdcdsc",
        //             pavvwowd:"sd"})
        //     {
        //         result
        //         accessToken
        //     }
        // }
        return new LoginOutput() { Result = "Y", AccessToken = "vdftghtrvbtrh", RefreshToken = "sdcthvrthytj" };
    }

    public IQueryable<Car> GetCars()
    {
        // query{
        //     cars{
        //         carNo
        //         brand
        //     }
        // }
        return new List<Car> {
            new Car() { CarNo = "AAA-1234", Lat = 25.123, Lng = 121.123, Price = 1000, CID = "c1", Type = "yaris", Brand = "Toyota", Type2 = "跑車" },
            new Car() { CarNo = "BBB-1234", Lat = 25.123, Lng = 121.123, Price = 1000, CID = "c2", Type = "city", Brand = "Honda", Type2 = "跑車" },
            new Car() { CarNo = "CCC-1234", Lat = 25.123, Lng = 121.123, Price = 1000, CID = "c3", Type = "sentra", Brand = "Nissan", Type2 = "跑車" },
            new Car() { CarNo = "DDD-1234", Lat = 25.123, Lng = 121.123, Price = 1000, CID = "c4", Type = "golf", Brand = "VW", Type2 = "跑車" },
            }.AsQueryable();
    }

    public IQueryable<MemberData> GetMemberData(string id)
    {
        // query{
        //     memberData(
        //         id:"cdcdc")
        //     {
        //         iD
        //         phone
        //     }
        // }
        return new List<MemberData> {
            new MemberData() { ID = "T123", Basvd = "12", Name = "", Email = "1@jotmail.com", Phone = "c1" },
            new MemberData() { ID = "E123", Basvd = "23", Name = "", Email = "2@jotmail.com", Phone = "c2" },
            new MemberData() { ID = "G123", Basvd = "34", Name = "", Email = "3@jotmail.com", Phone = "c3" },
            new MemberData() { ID = "S123", Basvd = "45", Name = "", Email = "4@jotmail.com", Phone = "c4" },
            }.AsQueryable();
    }
}