using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Update_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PalettModel_Ingredients_MaterialId",
                table: "PalettModel");

            migrationBuilder.DropIndex(
                name: "IX_PalettModel_MaterialId",
                table: "PalettModel");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "PalettModel");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "PalettModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PalettModel_IngredientId",
                table: "PalettModel",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PalettModel_Ingredients_IngredientId",
                table: "PalettModel",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PalettModel_Ingredients_IngredientId",
                table: "PalettModel");

            migrationBuilder.DropIndex(
                name: "IX_PalettModel_IngredientId",
                table: "PalettModel");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "PalettModel");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "PalettModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PalettModel_MaterialId",
                table: "PalettModel",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_PalettModel_Ingredients_MaterialId",
                table: "PalettModel",
                column: "MaterialId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
