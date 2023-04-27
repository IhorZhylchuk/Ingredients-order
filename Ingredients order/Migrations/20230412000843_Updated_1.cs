using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Updated_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataRealizacji",
                table: "OrdersForWarehouse");

            migrationBuilder.DropColumn(
                name: "DataZamówienia",
                table: "OrdersForWarehouse");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrdersForWarehouse");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NewOrders");

            migrationBuilder.AddColumn<string>(
                name: "DataRealizacji",
                table: "PalettModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataZamówienia",
                table: "PalettModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataRealizacji",
                table: "PalettModel");

            migrationBuilder.DropColumn(
                name: "DataZamówienia",
                table: "PalettModel");

            migrationBuilder.AddColumn<string>(
                name: "DataRealizacji",
                table: "OrdersForWarehouse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataZamówienia",
                table: "OrdersForWarehouse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrdersForWarehouse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "NewOrders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
