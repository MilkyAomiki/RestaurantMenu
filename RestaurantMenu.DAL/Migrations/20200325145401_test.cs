using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantMenu.DAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCalorific",
                table: "Dish",
                nullable: false,
                computedColumnSql: "2+2",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCalorific",
                table: "Dish",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComputedColumnSql: "2+2");
        }
    }
}
