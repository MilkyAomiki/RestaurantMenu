using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantMenu.DAL.Migrations
{
    public partial class tcalr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCalorific",
                table: "Dish",
                nullable: false,
                computedColumnSql: "[Calorific] * CAST((Cast([Gram] as decimal(18,2))/Cast(100 as decimal(18,2)) AS DECIMAL(18,2))",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "2+2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCalorific",
                table: "Dish",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "2+2",
                oldClrType: typeof(decimal),
                oldComputedColumnSql: "[Calorific] * CAST((Cast([Gram] as decimal(18,2))/Cast(100 as decimal(18,2)) AS DECIMAL(18,2))");
        }
    }
}
