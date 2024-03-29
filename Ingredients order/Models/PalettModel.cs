﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class PalettModel
    {
        public int Id { get; set; }
        public long PalletNumber { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Material { get; set; }
        public int Ilość { get; set; }
        public string Localization { get; set; }
        public string Status { get; set; }
        public int? NewOrderId { get; set; }
        public NewOrder NewOrder { get;set; }
        public int? OrdersForWarehouseId { get; set; }
        public OrdersForWarehouse OrdersForWarehouse { get; set; }
    }
}
