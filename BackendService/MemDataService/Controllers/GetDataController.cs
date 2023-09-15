using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemDataService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MemDataService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetDataController : ControllerBase
    {
        private IGetDataService _myService;
        public GetDataController(IGetDataService myService)
        {
            _myService = myService;
        }

        // [HttpPost]
        // public (string ID, string Name, string Email, string Phone) Post()
        // {
        //     return (_myService.GetUserData());
        //     // string a = "";
        //     // return (a, a, a, a);
        // }

        [HttpPost]
        public IActionResult Post()
        {
            return Content(System.Text.Json.JsonSerializer.Serialize(_myService.GetUserData()), "application/json");
        }
    }
}