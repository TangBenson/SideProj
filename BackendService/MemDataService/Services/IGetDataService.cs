using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemDataService.Models;

namespace MemDataService.Services
{
    public interface IGetDataService
    {
        MemberData GetUserData();
    }
}