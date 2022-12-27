using Ingredients_order.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ingredients_order.Models
{
    public static class DefaultRecipies
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            List<Ingredient> Ingredients = new List<Ingredient>()
            {
                new Ingredient {Id = 1, MaterialNumber = 4405021, Name = "Cukier"},
                new Ingredient {Id = 2, MaterialNumber = 4431245, Name = "Mleko zagęszczone"},
                new Ingredient {Id = 3, MaterialNumber = 4460655, Name = "Odpieniacz"},
                new Ingredient {Id = 4, MaterialNumber = 4433212, Name = "Konserwant"},
                new Ingredient {Id = 5, MaterialNumber = 4477132, Name = "Aromat Krówka"},
                new Ingredient {Id = 6, MaterialNumber = 4498443, Name = "Truskawka"},
                new Ingredient {Id = 7, MaterialNumber = 4453211, Name = "Skrobia modyfikowana"},
                new Ingredient {Id = 8, MaterialNumber = 4465543, Name = "Aromat truskawka"},
                new Ingredient {Id = 9, MaterialNumber = 4494328, Name = "Wiśnia"},
                new Ingredient {Id = 10, MaterialNumber = 4465503, Name = "Aromat wiśnia"},
                new Ingredient {Id = 11, MaterialNumber = 4475934, Name = "Guma Xantan"},
                new Ingredient {Id = 12, MaterialNumber = 4416630, Name = "Aromat waniliowy"},
                new Ingredient {Id = 13, Name = "Woda"},
                new Ingredient {Id = 14, MaterialNumber = 4409530, Name = "Syrop glukozowy"},

            };
            List<Recipe> Recipies = new List<Recipe>()
           {
               new Recipe{Id = 1, Name = "Sos krówka"},
               new Recipe {Id = 2, Name = "Truskawka w żelu"},
               new Recipe {Id = 3, Name = "Wiśnia w żelu"},
               new Recipe {Id = 4, Name = "Nadzienie waniliowe"}
           };

            List<Relation> relations = new List<Relation>()
           {
               new Relation{Id = 1, IngredientsId = 1, RecipeId = 1, Amount = 162},
               new Relation{Id = 2, IngredientsId = 2, RecipeId = 1, Amount = 430},
               new Relation{Id = 3, IngredientsId = 3, RecipeId = 1, Amount = 1.3},
               new Relation{Id = 4, IngredientsId = 4, RecipeId = 1, Amount = 2.2},
               new Relation{Id = 5, IngredientsId = 5, RecipeId = 1, Amount = 4.7},
               new Relation{Id = 6, IngredientsId = 13, RecipeId = 1, Amount = 400},


               new Relation{Id = 7, IngredientsId = 1, RecipeId = 2, Amount = 300},
               new Relation{Id = 8, IngredientsId = 4, RecipeId = 2, Amount = 42},
               new Relation{Id = 9, IngredientsId = 6, RecipeId = 2, Amount = 530},
               new Relation{Id = 10, IngredientsId = 7, RecipeId = 2, Amount = 2.7},
               new Relation{Id = 11, IngredientsId = 8, RecipeId = 2, Amount = 5.1},
               new Relation{Id = 12, IngredientsId = 13, RecipeId = 2, Amount = 120},


               new Relation{Id = 13, IngredientsId = 1, RecipeId = 3, Amount = 230},
               new Relation{Id = 14, IngredientsId = 4, RecipeId = 3, Amount = 4.2},
               new Relation{Id = 15, IngredientsId = 9, RecipeId = 3, Amount = 570},
               new Relation{Id = 16, IngredientsId = 7, RecipeId = 3, Amount = 40},
               new Relation{Id = 17, IngredientsId = 10,RecipeId = 3, Amount = 6.1},
               new Relation{Id = 18, IngredientsId = 13, RecipeId = 3, Amount = 150},


               new Relation{Id = 19, IngredientsId = 1, RecipeId = 4, Amount = 340},
               new Relation{Id = 20, IngredientsId = 4, RecipeId = 4, Amount = 3.6},
               new Relation{Id = 21, IngredientsId = 11, RecipeId = 4, Amount = 4.7},
               new Relation{Id = 22, IngredientsId = 7, RecipeId = 4, Amount = 120},
               new Relation{Id = 23, IngredientsId = 12, RecipeId = 4, Amount = 5.2},
               new Relation{Id = 24, IngredientsId = 13, RecipeId = 4, Amount = 250},
               new Relation{Id = 25, IngredientsId = 14, RecipeId = 4, Amount = 276.5},
           };
            List<Opakowania> opakowania = new List<Opakowania>()
            {
                new Opakowania{ Id = 1, MaterialNumber = 4439904, Name = "Butelka czarna 1 kg", Capacity = 1},
                new Opakowania{ Id = 2, MaterialNumber = 4477398, Name = "Wiadro białe 10 kg", Capacity = 10},
                new Opakowania{ Id = 3, MaterialNumber = 4033456, Name = "Wiadro czerwone 3.2 kg", Capacity = 3.2},
                new Opakowania{ Id = 4, MaterialNumber = 4499540, Name = "Nakrentka RD50"},
                new Opakowania{ Id = 5, MaterialNumber = 4432324, Name = "Wieczko niebeiske średnica 18 cm (3.2 kg)"},
                new Opakowania{ Id = 6, MaterialNumber = 4466950, Name = "Wieczko białe średnica 32 cm (10 kg)"},
                new Opakowania{ Id = 7, MaterialNumber = 4436904, Name = "Naklejka 100x100 biała"},
                new Opakowania{ Id = 8, MaterialNumber = 4410932, Name = "Naklejka Truskawka w żelu 3.2 kg"},
                new Opakowania{ Id = 9, MaterialNumber = 4490437, Name = "Naklejka Wiśnia w żelu 3.2 kg"},
                new Opakowania{ Id = 10, MaterialNumber = 4400475, Name = "Naklejka Sos Krówka 1 kg"}
            };
            modelBuilder.Entity<Opakowania>().HasData(opakowania);
            modelBuilder.Entity<Ingredient>().HasData(Ingredients);
            modelBuilder.Entity<Recipe>().HasData(Recipies);
            modelBuilder.Entity<Relation>().HasData(relations);

        }
        public static double Count(double totalCount, double item)
        {
            return Math.Round((totalCount / 1000) * item, 2);
        }
    }
}