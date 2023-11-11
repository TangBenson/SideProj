using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFCoreService.DbConnect
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Booking> Bookcar { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<MemberData> Member { get; set; }
        public DbSet<Token> Jwttoken { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(@"Server=127.0.0.1,1433;Database=RentCarDB;User Id=sa;Password=Aa111111;TrustServerCertificate=true");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(o => o.CarNo);
            modelBuilder.Entity<Token>().HasKey(o => o.Account);
            modelBuilder.Entity<MemberData>().HasKey(o => o.ID);
        }
    }

    /*
    classlib專案沒有startup或program設定builder.Services.AddDbContext
    因此要做以下設定。
    這種方法的核心思想是，使用一個工廠方法（CreateDbContext）在 Class Library 中創建 DbContext 實例，
    然後在 Web API 或其他應用程序中將它添加到服務集合中。
    ref:https://medium.com/oppr/net-core-using-entity-framework-core-in-a-separate-project-e8636f9dc9e5
    */
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../JWTService/appsettings.json")
                    .Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("SqlConStr");
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}