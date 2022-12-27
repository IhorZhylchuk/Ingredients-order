using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int NrZlecenia { get; set; }
        public string RecipesName { get; set; }
        public int RecipeId { get; set; }
        public double Count { get; set; }
        public string Opakowanie { get; set; }
        public string PokrywaNekrętka { get; set; }
        public string Naklejka { get; set; }
        public int IlośćOpakowań { get; set; }
        public int IlośćPokrywNekrętek { get; set; }
        public int IlośćNaklejek { get; set; }
    }
    public class ItemsCount
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int IngredientId { get; set; }
        public double IngredientCount { get; set; }
        
    }
}
