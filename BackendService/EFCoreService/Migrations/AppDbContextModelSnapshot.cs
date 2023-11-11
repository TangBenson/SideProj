﻿// <auto-generated />
using System;
using EFCoreService.DbConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreService.Models.Booking", b =>
                {
                    b.Property<string>("OrederNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("OrederDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrederNumber");

                    b.ToTable("Bookcar");
                });

            modelBuilder.Entity("EFCoreService.Models.Car", b =>
                {
                    b.Property<string>("CarNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lon")
                        .HasColumnType("float");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.HasKey("CarNo");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("EFCoreService.Models.MemberData", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Basvd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("EFCoreService.Models.Token", b =>
                {
                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshTokeno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Account");

                    b.ToTable("Jwttoken");
                });
#pragma warning restore 612, 618
        }
    }
}