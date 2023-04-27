using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Update_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "DataRealizacji",
                table: "OrdersForWarehouse");

            migrationBuilder.DropColumn(
                name: "DataZamówienia",
                table: "OrdersForWarehouse");

            migrationBuilder.DropColumn(
                name: "Palett",
                table: "OrdersForWarehouse");

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
    }
}
