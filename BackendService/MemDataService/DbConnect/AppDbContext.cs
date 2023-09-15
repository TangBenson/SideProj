using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemDataService.Models;
using Microsoft.EntityFrameworkCore;

namespace MemDataService.DbConnect
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MemberData> member { get; set; }
        public DbSet<MortorData> mortor { get; set; }
    }
}