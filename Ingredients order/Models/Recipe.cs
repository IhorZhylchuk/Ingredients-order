using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }


       // public ICollection<Ingredient> Ingredients { get; set;}
        //[NotMapped]
        //public List<double> Amount { get; set; }
    }
    public class Opakowania
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
