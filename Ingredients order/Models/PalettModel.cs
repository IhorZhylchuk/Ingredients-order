using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class PalettModel
    {
        public int Id { get; set; }
        public long PalletNumber { get; set; }
        public Ingredient Material { get; set; }
        public int Ilość { get; set; }
        public string Localization { get; set; }
        
    }
}
