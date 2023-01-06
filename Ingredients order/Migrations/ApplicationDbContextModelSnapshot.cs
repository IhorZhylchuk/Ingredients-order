﻿// <auto-generated />
using Ingredients_order.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ingredients_order.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ingredients_order.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Capacity")
                        .HasColumnType("float");

                    b.Property<int>("MaterialNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 0.0,
                            MaterialNumber = 4405021,
                            Name = "Cukier",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 0.0,
                            MaterialNumber = 4431245,
                            Name = "Mleko zagęszczone",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 0.0,
                            MaterialNumber = 4460655,
                            Name = "Odpieniacz",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 4,
                            Capacity = 0.0,
                            MaterialNumber = 4433212,
                            Name = "Konserwant",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 5,
                            Capacity = 0.0,
                            MaterialNumber = 4477132,
                            Name = "Aromat Krówka",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 6,
                            Capacity = 0.0,
                            MaterialNumber = 4498443,
                            Name = "Truskawka",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 7,
                            Capacity = 0.0,
                            MaterialNumber = 4453211,
                            Name = "Skrobia modyfikowana",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 8,
                            Capacity = 0.0,
                            MaterialNumber = 4465543,
                            Name = "Aromat truskawka",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 9,
                            Capacity = 0.0,
                            MaterialNumber = 4494328,
                            Name = "Wiśnia",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 10,
                            Capacity = 0.0,
                            MaterialNumber = 4465503,
                            Name = "Aromat wiśnia",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 11,
                            Capacity = 0.0,
                            MaterialNumber = 4475934,
                            Name = "Guma Xantan",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 12,
                            Capacity = 0.0,
                            MaterialNumber = 4416630,
                            Name = "Aromat waniliowy",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 13,
                            Capacity = 0.0,
                            MaterialNumber = 0,
                            Name = "Woda",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 14,
                            Capacity = 0.0,
                            MaterialNumber = 4409530,
                            Name = "Syrop glukozowy",
                            SectionName = "Składniki"
                        },
                        new
                        {
                            Id = 15,
                            Capacity = 1.0,
                            MaterialNumber = 4439904,
                            Name = "Butelka czarna 1 kg",
                            SectionName = "Opakowania",
                            Use = "Container"
                        },
                        new
                        {
                            Id = 16,
                            Capacity = 10.0,
                            MaterialNumber = 4477398,
                            Name = "Wiadro białe 10 kg",
                            SectionName = "Opakowania",
                            Use = "Container"
                        },
                        new
                        {
                            Id = 17,
                            Capacity = 3.2000000000000002,
                            MaterialNumber = 4033456,
                            Name = "Wiadro czerwone 3.2 kg",
                            SectionName = "Opakowania",
                            Use = "Container"
                        },
                        new
                        {
                            Id = 18,
                            Capacity = 0.0,
                            MaterialNumber = 4499540,
                            Name = "Nakrentka RD50",
                            SectionName = "Opakowania",
                            Use = "Cap"
                        },
                        new
                        {
                            Id = 19,
                            Capacity = 0.0,
                            MaterialNumber = 4432324,
                            Name = "Wieczko niebeiske średnica 18 cm (3.2 kg)",
                            SectionName = "Opakowania",
                            Use = "Cap"
                        },
                        new
                        {
                            Id = 20,
                            Capacity = 0.0,
                            MaterialNumber = 4466950,
                            Name = "Wieczko białe średnica 32 cm (10 kg)",
                            SectionName = "Opakowania",
                            Use = "Cap"
                        },
                        new
                        {
                            Id = 21,
                            Capacity = 0.0,
                            MaterialNumber = 4436904,
                            Name = "Naklejka 100x100 biała",
                            SectionName = "Opakowania",
                            Use = "Label"
                        },
                        new
                        {
                            Id = 22,
                            Capacity = 0.0,
                            MaterialNumber = 4410932,
                            Name = "Naklejka Truskawka w żelu 3.2 kg",
                            SectionName = "Opakowania",
                            Use = "Label"
                        },
                        new
                        {
                            Id = 23,
                            Capacity = 0.0,
                            MaterialNumber = 4490437,
                            Name = "Naklejka Wiśnia w żelu 3.2 kg",
                            SectionName = "Opakowania",
                            Use = "Label"
                        },
                        new
                        {
                            Id = 24,
                            Capacity = 0.0,
                            MaterialNumber = 4400475,
                            Name = "Naklejka Sos Krówka 1 kg",
                            SectionName = "Opakowania",
                            Use = "Label"
                        });
                });

            modelBuilder.Entity("Ingredients_order.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("IlośćNaklejek")
                        .HasColumnType("int");

                    b.Property<int>("IlośćOpakowań")
                        .HasColumnType("int");

                    b.Property<int>("IlośćPokrywNekrętek")
                        .HasColumnType("int");

                    b.Property<string>("Naklejka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NrZlecenia")
                        .HasColumnType("int");

                    b.Property<string>("Opakowanie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokrywaNekrętka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("RecipesName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Ingredients_order.Models.ItemsCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("IngredientCount")
                        .HasColumnType("float");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ItemsCount");
                });

            modelBuilder.Entity("Ingredients_order.Models.NewOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Count")
                        .HasColumnType("float");

                    b.Property<int>("IngredientNumber")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NewOrders");
                });

            modelBuilder.Entity("Ingredients_order.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sos krówka"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Truskawka w żelu"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Wiśnia w żelu"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Nadzienie waniliowe"
                        });
                });

            modelBuilder.Entity("Ingredients_order.Models.Relation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Relations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 162.0,
                            IngredientsId = 1,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 430.0,
                            IngredientsId = 2,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 1.3,
                            IngredientsId = 3,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 4,
                            Amount = 2.2000000000000002,
                            IngredientsId = 4,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 5,
                            Amount = 4.7000000000000002,
                            IngredientsId = 5,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 6,
                            Amount = 400.0,
                            IngredientsId = 13,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 7,
                            Amount = 300.0,
                            IngredientsId = 1,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 8,
                            Amount = 42.0,
                            IngredientsId = 4,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 9,
                            Amount = 530.0,
                            IngredientsId = 6,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 10,
                            Amount = 2.7000000000000002,
                            IngredientsId = 7,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 11,
                            Amount = 5.0999999999999996,
                            IngredientsId = 8,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 12,
                            Amount = 120.0,
                            IngredientsId = 13,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 13,
                            Amount = 230.0,
                            IngredientsId = 1,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 14,
                            Amount = 4.2000000000000002,
                            IngredientsId = 4,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 15,
                            Amount = 570.0,
                            IngredientsId = 9,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 16,
                            Amount = 40.0,
                            IngredientsId = 7,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 17,
                            Amount = 6.0999999999999996,
                            IngredientsId = 10,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 18,
                            Amount = 150.0,
                            IngredientsId = 13,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 19,
                            Amount = 340.0,
                            IngredientsId = 1,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 20,
                            Amount = 3.6000000000000001,
                            IngredientsId = 4,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 21,
                            Amount = 4.7000000000000002,
                            IngredientsId = 11,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 22,
                            Amount = 120.0,
                            IngredientsId = 7,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 23,
                            Amount = 5.2000000000000002,
                            IngredientsId = 12,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 24,
                            Amount = 250.0,
                            IngredientsId = 13,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 25,
                            Amount = 276.5,
                            IngredientsId = 14,
                            RecipeId = 4
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
