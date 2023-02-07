using Ingredients_order.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Controllers
{
    public class MachineProcessController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MachineProcessController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult ProcessMachineSelecting()
        {
            try {
                var listOfProcesses = _dbContext.Processes.Select(p => p).ToList();
                var zlecenia = _dbContext.Items.Select(n => n.NrZlecenia).ToList();
                ViewBag.Processes = new SelectList(listOfProcesses, "Id", "Name");
                ViewBag.Zlecenia = new SelectList(zlecenia);
                return View();
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }
        public JsonResult MachineList(int processId)
        {
            //var machines = _dbContext.Machines.Where(p => p.ProcessModelId == processId).Select(m => m.Name.Replace("\"", string.Empty).Trim()).ToList();
            var machines = _dbContext.Machines.Where(p => p.ProcessModelId == processId).Select(m => m).ToList();
            //return Json(machines);
            return Json(new { id = machines.Select(i => i.Id).ToList(), name = machines.Select(n => n.Name.Replace("\"", string.Empty).Trim()).ToList() }) ;
        }
         public JsonResult GetOrdersBySelect(int numerZlecenia, int processId, int machineId)
       // public JsonResult GetOrdersBySelect()

        {

            try {
                var nOrders = _dbContext.NewOrders.Where(n => n.ItemId == numerZlecenia && n.ProcessId == processId && n.MachineId == machineId).Select(o => o).ToList();
                List<string> ingredients = new List<string>();

                foreach (var item in nOrders)
                {
                    ingredients.Add(_dbContext.Ingredients.Where(n => n.MaterialNumber == item.IngredientNumber).Select(n => n.Name).FirstOrDefault());
                }

                List<Tuple<int, string, double, string, int>> orders = new List<Tuple<int, string, double, string, int>>();

                for (var i = 0; i < nOrders.Count(); i++)
                {
                    orders.Add(new Tuple<int, string, double, string, int>(nOrders.Select(n => n.IngredientNumber).ToList()[i], ingredients[i], nOrders.Select(n => n.Count).ToList()[i], nOrders.Select(s => s.Status).ToList()[i], nOrders.Select(i => i.Id).ToList()[i]));
                }

                return Json(orders);
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return Json(null);
            
        }

        public JsonResult GetOrders()
        {
            var nOrders = _dbContext.NewOrders.Select(o => o).ToList();
            List<string> ingredients = new List<string>();

            foreach (var item in nOrders)
            {
                ingredients.Add(_dbContext.Ingredients.Where(n => n.MaterialNumber == item.IngredientNumber).Select(n => n.Name).FirstOrDefault());
            }

            List<Tuple<int, string, double, string, int>> orders = new List<Tuple<int, string, double, string, int>>();

            for (var i = 0; i < nOrders.Count(); i++)
            {
                orders.Add(new Tuple<int, string, double, string, int>(nOrders.Select(n => n.IngredientNumber).ToList()[i], ingredients[i], nOrders.Select(n => n.Count).ToList()[i], nOrders.Select(s => s.Status).ToList()[i], nOrders.Select(i => i.Id).ToList()[i]));
            }

            return Json(orders);
        }
        public JsonResult GetIngredients()
        {
            var zlecenie = _dbContext.Items.Select(i => i).FirstOrDefault();
            try
            {
                var orders = _dbContext.NewOrders.Where(i => i.ItemId == zlecenie.NrZlecenia).Select(n => n.IngredientNumber);
                var surowce = _dbContext.Relations.Where(i => i.RecipeId == zlecenie.RecipeId).Where(i => i.IngredientsId != 13).Select(i => i.IngredientsId).ToList();

                List<Ingredient> opakowaniaList = new List<Ingredient>()
                {
                     _dbContext.Ingredients.Where(n => n.Name == zlecenie.Opakowanie && orders.Contains(n.MaterialNumber)== false).Select(o => o).FirstOrDefault(),
                     _dbContext.Ingredients.Where(n => n.Name == zlecenie.PokrywaNekrętka && orders.Contains(n.MaterialNumber)== false).Select(o => o).FirstOrDefault(),
                     _dbContext.Ingredients.Where(n => n.Name == zlecenie.Naklejka && orders.Contains(n.MaterialNumber)== false).Select(o => o).FirstOrDefault()

                };

                List<Tuple<int, string, double>> ingredients = new List<Tuple<int, string, double>>();

                foreach (var elem in surowce)
                {
                    var ingredient = _dbContext.Ingredients.Where(i => i.Id == elem && orders.Contains(i.MaterialNumber) == false).Select(i => i).FirstOrDefault();
                    try
                    {
                        var counts = _dbContext.ItemsCount.Where(i => i.IngredientId == ingredient.Id).Select(i => i.IngredientCount).FirstOrDefault();
                        ingredients.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));

                    }
                    catch (Exception e)
                    {
                        e.Message.ToString();
                    }

                };
                if (ingredients.Count() != 0)
                {
                    return Json(new { details = zlecenie, opakowania = opakowaniaList, items = ingredients, num = 1, onOrders = orders });
                }
                else if (opakowaniaList.Where(i => i != null).ToList().Count() != 0)
                {
                    return Json(new { details = zlecenie, opakowania = opakowaniaList, items = ingredients, num = 1, onOrders = orders });
                }
                else
                {
                    return Json(new { details = zlecenie, num = 0 });
                }

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return Json(new { details = zlecenie, num = 0 });
        }
        public JsonResult GetСount(string id)
        {
            var request = id.Split(',');
            var count = _dbContext.NewOrders.Where(n => n.IngredientNumber == Int32.Parse(request[0])).Select(c => c.Count).FirstOrDefault();
            try
            {
                if (Int32.Parse(request[1]) != 0)
                {
                    if ((Int32.Parse(request[1]) / count) * 100 <= 20)
                    {
                        return Json(1);
                    }
                    else
                    {
                        return Json(2);
                    }
                }
                return Json(3);
            }
            catch (Exception e)
            {
                return Json(3);
            }

        }
        public IActionResult PostMethod([FromBody] IEnumerable<Order> orders)
        {
            if (orders != null)
            {
                try
                {
                    foreach (var order in orders)
                    {
                        NewOrder newOrder = new NewOrder();

                        newOrder.IngredientNumber = Int32.Parse(order.Ingredient);
                        newOrder.ItemId = order.ItemId;
                        newOrder.MachineId = order.MachineId;
                        newOrder.ProcessId = order.ProcessId;
                        newOrder.Status = "W trakcie realizacji";

                        var ingredient = _dbContext.Ingredients.Where(i => i.MaterialNumber == Int32.Parse(order.Ingredient)).Select(i => i).FirstOrDefault();
                        if (ingredient.SectionName == "Składniki")
                        {
                            var count = _dbContext.ItemsCount.Where(i => i.IngredientId == ingredient.Id).Select(i => i.IngredientCount).FirstOrDefault();
                            newOrder.Count = count;
                        }
                        else
                        {
                            newOrder.Count = _dbContext.Items.Where(i => i.NrZlecenia == order.ItemId).Select(o => o.IlośćOpakowań).FirstOrDefault();
                        }

                        _dbContext.NewOrders.Add(newOrder);
                        OrdersForWarehouse defaultForOrders = new OrdersForWarehouse();
                        defaultForOrders.Id = newOrder.Id;
                        defaultForOrders.IngredientNumber = newOrder.IngredientNumber;
                        defaultForOrders.ItemId = newOrder.ItemId;
                        defaultForOrders.MachineId = newOrder.MachineId;
                        defaultForOrders.ProcessId = newOrder.ProcessId;
                        defaultForOrders.Status = newOrder.Status;
                        defaultForOrders.Count = newOrder.Count;
                        defaultForOrders.DataZamówienia = DateTime.Now.ToString();

                        _dbContext.OrdersForWarehouse.Add(defaultForOrders);
                        _dbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
                return Ok();
            }
            return NotFound();


        }
        public IActionResult SetByNumberInput([FromBody] IEnumerable<Order> orders)
        {
            if (orders != null)
            {
                try
                {
                    NewOrder newOrder = new NewOrder();

                    foreach (var order in orders)
                    {
                        newOrder.IngredientNumber = Int32.Parse(order.Ingredient);
                        newOrder.ItemId = order.ItemId;
                        newOrder.MachineId = order.MachineId;
                        newOrder.ProcessId = order.ProcessId;
                        newOrder.Status = "W trakcie realizacji";
                        newOrder.Count = order.Count;

                        _dbContext.NewOrders.Add(newOrder);
                        _dbContext.SaveChanges();
                    }


                    return Json(new { message = newOrder });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }

            return Json(new { message = "Error!" });
        }




    }
}
