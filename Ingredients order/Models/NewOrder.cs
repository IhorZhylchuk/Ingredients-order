using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class NewOrder: DefaultForOrders
    {

    }
    public class OrdersForWarehouse : DefaultForOrders
    {
        public string DataZamówienia { get; set; }
        public string DataRealizacji { get; set; }
    }
    public class DefaultForOrders
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ProcessId { get; set; }
        public int MachineId { get; set; }
        public string Status { get; set; }
        public int IngredientNumber { get; set; }
        public double Count { get; set; }
    }
}
