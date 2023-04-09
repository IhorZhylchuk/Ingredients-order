using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Initial_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Palett",
                table: "OrdersForWarehouse",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Palett",
                table: "OrdersForWarehouse");
        }
    }
}
