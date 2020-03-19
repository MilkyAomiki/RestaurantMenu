using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantMenu.DAL.Migrations
{
    public partial class ChangedPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Dish",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "smallmoney");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Dish",
                type: "smallmoney",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
