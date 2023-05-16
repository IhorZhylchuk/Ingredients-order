using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingredients_order.Migrations
{
    public partial class Initial_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Use = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<double>(type: "float", nullable: false)
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
                    IngredientNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdersForWarehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataZamówienia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRealizacji = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Palett = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    IngredientNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersForWarehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PalettModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PalletNumber = table.Column<long>(type: "bigint", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Ilość = table.Column<int>(type: "int", nullable: false),
                    Localization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewOrderId = table.Column<int>(type: "int", nullable: true),
                    OrdersForWarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalettModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PalettModel_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PalettModel_NewOrders_NewOrderId",
                        column: x => x.NewOrderId,
                        principalTable: "NewOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PalettModel_OrdersForWarehouse_OrdersForWarehouseId",
                        column: x => x.OrdersForWarehouseId,
                        principalTable: "OrdersForWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_Processes_ProcessModelId",
                        column: x => x.ProcessModelId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BinNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    BinStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bins_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a12be9c5-aa65-4af6-bd97-00bd9344e575", "f805f473-f6e9-4af7-98bc-ce9492b603d8", "NormalUser", null },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "731ed15c-2220-449d-b23a-43ef1b2eaa24", "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a12be9c5-aa65-4af6-bd97-00bd9344e575", 0, "5afc610d-42c9-49c9-a5f3-69d110bfedc3", "petro@gmail.com", true, false, null, "petro@gmail.com", "Petro", "AQAAAAEAACcQAAAAECtWUYaCyzUPsiIZM8+N10brXxfjeuEghpZIXNy051hB/ZBVmEeyNIg+WjIHf1+C8A==", null, false, "", false, "Petro" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, "36b4c701-25a5-4b2f-ace6-9c5c11459daa", "sara@gmail.com", true, false, null, "sara@gmail.com", "Sara", "AQAAAAEAACcQAAAAEBjXqAuibzCziw9z0sM6PEowND7Wdy/kSC3EsWavfQLogTvpQRzy2x+YdHIi4xmZNw==", null, false, "", false, "Sara" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Capacity", "MaterialNumber", "Name", "SectionName", "Use" },
                values: new object[,]
                {
                    { 24, 0.0, 4400475, "Naklejka Sos Krówka 1 kg", "Opakowania", "Label" },
                    { 23, 0.0, 4490437, "Naklejka Wiśnia w żelu 3.2 kg", "Opakowania", "Label" },
                    { 22, 0.0, 4410932, "Naklejka Truskawka w żelu 3.2 kg", "Opakowania", "Label" },
                    { 21, 0.0, 4436904, "Naklejka 100x100 biała", "Opakowania", "Label" },
                    { 20, 0.0, 4466950, "Wieczko białe średnica 32 cm (10 kg)", "Opakowania", "Cap" },
                    { 19, 0.0, 4432324, "Wieczko niebeiske średnica 18 cm (3.2 kg)", "Opakowania", "Cap" },
                    { 18, 0.0, 4499540, "Nakrentka RD50", "Opakowania", "Cap" },
                    { 17, 3.2000000000000002, 4033456, "Wiadro czerwone 3.2 kg", "Opakowania", "Container" },
                    { 16, 10.0, 4477398, "Wiadro białe 10 kg", "Opakowania", "Container" },
                    { 15, 1.0, 4439904, "Butelka czarna 1 kg", "Opakowania", "Container" },
                    { 1, 0.0, 4405021, "Cukier", "Składniki", null },
                    { 13, 0.0, 0, "Woda", "Składniki", null },
                    { 3, 0.0, 4460655, "Odpieniacz", "Składniki", null },
                    { 2, 0.0, 4431245, "Mleko zagęszczone", "Składniki", null },
                    { 4, 0.0, 4433212, "Konserwant", "Składniki", null },
                    { 6, 0.0, 4498443, "Truskawka", "Składniki", null },
                    { 7, 0.0, 4458216, "Skrobia modyfikowana", "Składniki", null },
                    { 5, 0.0, 4477132, "Aromat Krówka", "Składniki", null },
                    { 9, 0.0, 4494328, "Wiśnia", "Składniki", null },
                    { 10, 0.0, 4465503, "Aromat wiśnia", "Składniki", null },
                    { 11, 0.0, 4475934, "Guma Xantan", "Składniki", null },
                    { 12, 0.0, 4416630, "Aromat waniliowy", "Składniki", null },
                    { 8, 0.0, 4465543, "Aromat truskawka", "Składniki", null },
                    { 14, 0.0, 4409530, "Syrop glukozowy", "Składniki", null }
                });

            migrationBuilder.InsertData(
                table: "Processes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Process Area" },
                    { 2, "Packing Area" },
                    { 3, "Warehouse" },
                    { 4, "Dry good's Area" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Wiśnia w żelu" },
                    { 1, "Sos krówka" },
                    { 2, "Truskawka w żelu" },
                    { 4, "Nadzienie waniliowe" }
                });

            migrationBuilder.InsertData(
                table: "Relations",
                columns: new[] { "Id", "Amount", "IngredientsId", "RecipeId" },
                values: new object[,]
                {
                    { 2, 430.0, 2, 1 },
                    { 25, 276.5, 14, 4 },
                    { 24, 250.0, 13, 4 },
                    { 23, 5.2000000000000002, 12, 4 },
                    { 22, 120.0, 7, 4 },
                    { 21, 4.7000000000000002, 11, 4 }
                });

            migrationBuilder.InsertData(
                table: "Relations",
                columns: new[] { "Id", "Amount", "IngredientsId", "RecipeId" },
                values: new object[,]
                {
                    { 20, 3.6000000000000001, 4, 4 },
                    { 19, 340.0, 1, 4 },
                    { 18, 150.0, 13, 3 },
                    { 17, 6.0999999999999996, 10, 3 },
                    { 16, 40.0, 7, 3 },
                    { 15, 570.0, 9, 3 },
                    { 14, 4.2000000000000002, 4, 3 },
                    { 13, 230.0, 1, 3 },
                    { 12, 120.0, 13, 2 },
                    { 11, 5.0999999999999996, 8, 2 },
                    { 10, 2.7000000000000002, 7, 2 },
                    { 9, 530.0, 6, 2 },
                    { 8, 42.0, 4, 2 },
                    { 7, 300.0, 1, 2 },
                    { 5, 4.7000000000000002, 5, 1 },
                    { 4, 2.2000000000000002, 4, 1 },
                    { 3, 1.3, 3, 1 },
                    { 1, 162.0, 1, 1 },
                    { 6, 400.0, 13, 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                    { "a12be9c5-aa65-4af6-bd97-00bd9344e575", "a12be9c5-aa65-4af6-bd97-00bd9344e575" }
                });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "Name", "ProcessModelId" },
                values: new object[,]
                {
                    { 1, "Process machine № 1", 1 },
                    { 2, "Process machine № 2", 1 },
                    { 3, "Process machine № 3", 1 },
                    { 4, "Packing machine № 1", 2 },
                    { 5, "Packing machine № 2", 2 },
                    { 6, "Packing machine № 3", 2 },
                    { 7, "Packing machine № 4", 2 },
                    { 8, "Packing machine № 5", 2 },
                    { 9, "Area № 1", 3 },
                    { 10, "Area № 2", 3 },
                    { 11, "Dry good's Area", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bins_MachineId",
                table: "Bins",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_ProcessModelId",
                table: "Machines",
                column: "ProcessModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PalettModel_IngredientId",
                table: "PalettModel",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_PalettModel_NewOrderId",
                table: "PalettModel",
                column: "NewOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PalettModel_OrdersForWarehouseId",
                table: "PalettModel",
                column: "OrdersForWarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bins");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemsCount");

            migrationBuilder.DropTable(
                name: "PalettModel");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "NewOrders");

            migrationBuilder.DropTable(
                name: "OrdersForWarehouse");

            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
