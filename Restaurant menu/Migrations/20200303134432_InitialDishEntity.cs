using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant_menu.Migrations
{
    public partial class InitialDishEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dish",
                columns: table => new
                {
                    ID = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Consist = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "smallmoney", nullable: false),
                    Gram = table.Column<int>(nullable: false),
                    Calorific = table.Column<double>(nullable: false),
                    CookTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dish");
        }
    }
}
