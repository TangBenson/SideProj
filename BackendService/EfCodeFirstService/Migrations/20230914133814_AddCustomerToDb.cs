using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCodeFirstService.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bookcar",
                columns: table => new
                {
                    OrederNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrederDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookcar", x => x.OrederNumber);
                });

            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    CarNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.CarNo);
                });

            migrationBuilder.CreateTable(
                name: "jwttoken",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokeno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jwttoken", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "member",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Basvd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookcar");

            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "jwttoken");

            migrationBuilder.DropTable(
                name: "member");
        }
    }
}
