using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Initial_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrZlecenia = table.Column<int>(type: "int", nullable: false),
                    RecipesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Opakowanie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokrywaNekrętka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naklejka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlośćOpakowań = table.Column<int>(type: "int", nullable: false),
                    IlośćPokrywNekrętek = table.Column<int>(type: "int", nullable: false),
                    IlośćNaklejek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    IngredientCount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IngredientNumber = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opakowania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialNumber = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: false)
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
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "MaterialNumber", "Name" },
                values: new object[,]
                {
                    { 1, 4405021, "Cukier" },
                    { 14, 4409530, "Syrop glukozowy" },
                    { 12, 4416630, "Aromat waniliowy" },
                    { 11, 4475934, "Guma Xantan" },
                    { 10, 4465503, "Aromat wiśnia" },
                    { 9, 4494328, "Wiśnia" },
                    { 8, 4465543, "Aromat truskawka" },
                    { 13, 0, "Woda" },
                    { 6, 4498443, "Truskawka" },
                    { 5, 4477132, "Aromat Krówka" },
                    { 4, 4433212, "Konserwant" },
                    { 3, 4460655, "Odpieniacz" },
                    { 2, 4431245, "Mleko zagęszczone" },
                    { 7, 4453211, "Skrobia modyfikowana" }
                });

            migrationBuilder.InsertData(
                table: "Opakowania",
                columns: new[] { "Id", "Capacity", "MaterialNumber", "Name" },
                values: new object[,]
                {
                    { 9, 0.0, 4490437, "Naklejka Wiśnia w żelu 3.2 kg" },
                    { 8, 0.0, 4410932, "Naklejka Truskawka w żelu 3.2 kg" },
                    { 7, 0.0, 4436904, "Naklejka 100x100 biała" },
                    { 6, 0.0, 4466950, "Wieczko białe średnica 32 cm (10 kg)" },
                    { 3, 3.2000000000000002, 4033456, "Wiadro czerwone 3.2 kg" },
                    { 4, 0.0, 4499540, "Nakrentka RD50" },
                    { 2, 10.0, 4477398, "Wiadro białe 10 kg" },
                    { 1, 1.0, 4439904, "Butelka czarna 1 kg" },
                    { 10, 0.0, 4400475, "Naklejka Sos Krówka 1 kg" },
                    { 5, 0.0, 4432324, "Wieczko niebeiske średnica 18 cm (3.2 kg)" }
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
                columns: new[] { "Id", "Amount", "IngredientsId", "RecipeId" },
                values: new object[,]
                {
                    { 24, 250.0, 13, 4 },
                    { 23, 5.2000000000000002, 12, 4 },
                    { 22, 120.0, 7, 4 },
                    { 21, 4.7000000000000002, 11, 4 },
                    { 20, 3.6000000000000001, 4, 4 },
                    { 19, 340.0, 1, 4 },
                    { 18, 150.0, 13, 3 },
                    { 17, 6.0999999999999996, 10, 3 },
                    { 16, 40.0, 7, 3 },
                    { 15, 570.0, 9, 3 },
                    { 14, 4.2000000000000002, 4, 3 },
                    { 13, 230.0, 1, 3 },
                    { 12, 120.0, 13, 2 },
                    { 10, 2.7000000000000002, 7, 2 }
                });

            migrationBuilder.InsertData(
                table: "Relations",
                columns: new[] { "Id", "Amount", "IngredientsId", "RecipeId" },
                values: new object[,]
                {
                    { 9, 530.0, 6, 2 },
                    { 8, 42.0, 4, 2 },
                    { 7, 300.0, 1, 2 },
                    { 6, 400.0, 13, 1 },
                    { 5, 4.7000000000000002, 5, 1 },
                    { 4, 2.2000000000000002, 4, 1 },
                    { 3, 1.3, 3, 1 },
                    { 2, 430.0, 2, 1 },
                    { 1, 162.0, 1, 1 },
                    { 11, 5.0999999999999996, 8, 2 },
                    { 25, 276.5, 14, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemsCount");

            migrationBuilder.DropTable(
                name: "NewOrders");

            migrationBuilder.DropTable(
                name: "Opakowania");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Relations");
        }
    }
}
