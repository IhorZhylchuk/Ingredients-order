using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public int IngredientsId { get; set; }
        public int RecipeId { get; set; }
        public double Amount { get; set; }
        public string Opakowanie { get; set; }
        public string WieczkoNakrętka { get; set; }
        public string Folia { get; set; }
        public string Etykieta { get; set; }
    }
}
