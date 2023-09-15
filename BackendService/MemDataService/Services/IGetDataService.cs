using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemDataService.Services
{
    public interface IGetDataService
    {
        (string ID, string Name, string Email, string Phone) GetUserData();
    }
}