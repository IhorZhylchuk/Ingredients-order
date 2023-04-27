using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class Order
    {

        public int ItemId { get; set; }
        public int ProcessId { get; set; }
        public int MachineId { get; set; }
        public string Ingredient { get; set; }
        public int Count { get; set; }
    }
}
