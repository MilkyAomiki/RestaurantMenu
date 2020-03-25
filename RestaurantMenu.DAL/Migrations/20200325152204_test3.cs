using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantMenu.DAL.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCalorific",
                table: "Dish",
                nullable: false,
                computedColumnSql: "[Calorific] * Convert(decimal(18,2), [Gram]) / Convert(decimal(18,2), 100)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[Calorific] * CAST(([Gram] / 100) AS DECIMAL(18,2)))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCalorific",
                table: "Dish",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Calorific] * CAST(([Gram] / 100) AS DECIMAL(18,2)))",
                oldClrType: typeof(decimal),
                oldComputedColumnSql: "[Calorific] * Convert(decimal(18,2), [Gram]) / Convert(decimal(18,2), 100)");
        }
    }
}
