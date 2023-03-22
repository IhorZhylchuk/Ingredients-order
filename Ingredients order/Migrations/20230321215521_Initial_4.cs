using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Initial_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PalletNUmber",
                table: "PalettModel",
                newName: "PalletNumber");

            migrationBuilder.AlterColumn<long>(
                name: "PalletNumber",
                table: "PalettModel",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PalletNumber",
                table: "PalettModel",
                newName: "PalletNUmber");

            migrationBuilder.AlterColumn<int>(
                name: "PalletNUmber",
                table: "PalettModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
