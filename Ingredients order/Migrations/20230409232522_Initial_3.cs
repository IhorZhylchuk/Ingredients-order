using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Initial_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PalettModel_NewOrders_NewOrderId",
                table: "PalettModel");

            migrationBuilder.AlterColumn<int>(
                name: "NewOrderId",
                table: "PalettModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PalettModel_NewOrders_NewOrderId",
                table: "PalettModel",
                column: "NewOrderId",
                principalTable: "NewOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PalettModel_NewOrders_NewOrderId",
                table: "PalettModel");

            migrationBuilder.AlterColumn<int>(
                name: "NewOrderId",
                table: "PalettModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PalettModel_NewOrders_NewOrderId",
                table: "PalettModel",
                column: "NewOrderId",
                principalTable: "NewOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
