using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrZlecenia = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    RecipesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<double>(type: "float", nullable: false),
                    Opakowanie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokrywaNekrętka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naklejka = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opakowania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opakowania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Opakowanie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WieczkoNakrętka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Folia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Etykieta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "MaterialNumber", "Name" },
                values: new object[,]
                {
                    { 1, 4405021, "Cukier" },
                    { 11, 4475934, "Guma Xantan" },
                    { 10, 4465503, "Aromat wiśnia" },
                    { 9, 4494328, "Wiśnia" },
                    { 8, 4465543, "Aromat truskawka" },
                    { 7, 4453211, "Skrobia modyfikowana" },
                    { 12, 4416630, "Aromat waniliowy" },
                    { 5, 4477132, "Aromat Krówka" },
                    { 4, 4433212, "Konserwant" },
                    { 3, 4460655, "Odpieniacz" },
                    { 2, 4431245, "Mleko zagęszczone" },
                    { 6, 4498443, "Truskawka" }
                });

            migrationBuilder.InsertData(
                table: "Opakowania",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 10, "Naklejka Sos Krówka 1 kg" },
                    { 9, "Naklejka Wiśnia w żelu 3.2 kg" },
                    { 8, "Naklejka Truskawka w żelu 3.2 kg" },
                    { 7, "Naklejka 100x100 biała" },
                    { 6, "Wieczko białe średnica 32 cm (10 kg)" },
                    { 4, "Nakrentka RD50" },
                    { 3, "Wiadro czerwone 3.2 kg" },
                    { 2, "Wiadro białe 10 kg" },
                    { 1, "Butelka czarna 1 kg" },
                    { 5, "Wieczko niebeiske średnica 18 cm (3.2 kg)" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Nadzienie waniliowe" },
                    { 3, "Wiśnia w żelu" },
                    { 1, "Sos krówka" },
                    { 2, "Truskawka w żelu" }
                });

            migrationBuilder.InsertData(
                table: "Relations",
                columns: new[] { "Id", "Amount", "Etykieta", "Folia", "IngredientsId", "Opakowanie", "RecipeId", "WieczkoNakrętka" },
                values: new object[,]
                {
                    { 19, 25.0, null, null, 7, null, 4, null },
                    { 18, 0.69999999999999996, null, null, 11, null, 4, null },
                    { 17, 3.6000000000000001, null, null, 4, null, 4, null },
                    { 16, 150.0, null, null, 1, null, 4, null },
                    { 15, 6.0999999999999996, null, null, 10, null, 3, null },
                    { 14, 52.0, null, null, 7, null, 3, null },
                    { 13, 700.0, null, null, 9, null, 3, null },
                    { 12, 4.2000000000000002, null, null, 4, null, 3, null },
                    { 11, 270.0, null, null, 1, null, 3, null },
                    { 10, 5.0999999999999996, null, null, 8, null, 2, null },
                    { 8, 530.0, null, null, 6, null, 2, null },
                    { 7, 30.0, null, null, 4, null, 2, null },
                    { 6, 300.0, null, null, 1, null, 2, null },
                    { 5, 4.7000000000000002, null, null, 5, null, 1, null },
                    { 4, 2.2000000000000002, null, null, 4, null, 1, null },
                    { 3, 0.29999999999999999, null, null, 3, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Relations",
                columns: new[] { "Id", "Amount", "Etykieta", "Folia", "IngredientsId", "Opakowanie", "RecipeId", "WieczkoNakrętka" },
                values: new object[,]
                {
                    { 2, 250.0, null, null, 2, null, 1, null },
                    { 1, 180.0, null, null, 1, null, 1, null },
                    { 9, 2.7000000000000002, null, null, 7, null, 2, null },
                    { 20, 5.2000000000000002, null, null, 12, null, 4, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Opakowania");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Relations");
        }
    }
}
