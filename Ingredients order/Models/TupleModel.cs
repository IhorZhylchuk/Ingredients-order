using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class TupleModel
    {
        public int IngredientNumber { get; set; }
        public string IngredientName { get; set; }
        public double Count { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }
        public string DataZamówienia { get; set; }
        public string DataRealizacji { get; set; }
        public long NumerPalety { get; set; }

    }
}
