using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCodeFirstService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfCodeFirstService.DbConnect
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Booking> bookcar { get; set; }
        public DbSet<Car> car { get; set; }
        public DbSet<MemberData> member { get; set; }
        public DbSet<Token> jwttoken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(o => o.CarNo);
            modelBuilder.Entity<Token>().HasKey(o => o.Account);
            modelBuilder.Entity<MemberData>().HasKey(o => o.ID);
        }
    }
}