using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class Ingredient:Default
    {
        public override int Id { get; set; }
        public int MaterialNumber { get; set; }
        public override string Name { get; set; }

    }
}
