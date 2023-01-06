using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Initial_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opakowania");

            migrationBuilder.AddColumn<double>(
                name: "Capacity",
                table: "Ingredients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SectionName",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Use",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 13,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14,
                column: "SectionName",
                value: "Składniki");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Capacity", "MaterialNumber", "Name", "SectionName", "Use" },
                values: new object[,]
                {
                    { 23, 0.0, 4490437, "Naklejka Wiśnia w żelu 3.2 kg", "Opakowania", "Label" },
                    { 22, 0.0, 4410932, "Naklejka Truskawka w żelu 3.2 kg", "Opakowania", "Label" },
                    { 21, 0.0, 4436904, "Naklejka 100x100 biała", "Opakowania", "Label" },
                    { 20, 0.0, 4466950, "Wieczko białe średnica 32 cm (10 kg)", "Opakowania", "Cap" },
                    { 19, 0.0, 4432324, "Wieczko niebeiske średnica 18 cm (3.2 kg)", "Opakowania", "Cap" },
                    { 18, 0.0, 4499540, "Nakrentka RD50", "Opakowania", "Cap" },
                    { 17, 3.2000000000000002, 4033456, "Wiadro czerwone 3.2 kg", "Opakowania", "Container" },
                    { 16, 10.0, 4477398, "Wiadro białe 10 kg", "Opakowania", "Container" },
                    { 24, 0.0, 4400475, "Naklejka Sos Krówka 1 kg", "Opakowania", "Label" },
                    { 15, 1.0, 4439904, "Butelka czarna 1 kg", "Opakowania", "Container" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "SectionName",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Use",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "Opakowania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<double>(type: "float", nullable: false),
                    MaterialNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opakowania", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Opakowania",
                columns: new[] { "Id", "Capacity", "MaterialNumber", "Name" },
                values: new object[,]
                {
                    { 1, 1.0, 4439904, "Butelka czarna 1 kg" },
                    { 2, 10.0, 4477398, "Wiadro białe 10 kg" },
                    { 3, 3.2000000000000002, 4033456, "Wiadro czerwone 3.2 kg" },
                    { 4, 0.0, 4499540, "Nakrentka RD50" },
                    { 5, 0.0, 4432324, "Wieczko niebeiske średnica 18 cm (3.2 kg)" },
                    { 6, 0.0, 4466950, "Wieczko białe średnica 32 cm (10 kg)" },
                    { 7, 0.0, 4436904, "Naklejka 100x100 biała" },
                    { 8, 0.0, 4410932, "Naklejka Truskawka w żelu 3.2 kg" },
                    { 9, 0.0, 4490437, "Naklejka Wiśnia w żelu 3.2 kg" },
                    { 10, 0.0, 4400475, "Naklejka Sos Krówka 1 kg" }
                });
        }
    }
}
