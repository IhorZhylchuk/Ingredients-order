using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public string Area { get; set; }
        public string Status { get; set; }
    }
}
