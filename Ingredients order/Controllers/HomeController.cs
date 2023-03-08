using Ingredients_order.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public JsonResult GetZlecenie(int numerZlecenia)
        {
            var search = _dbContext.Items.Find(numerZlecenia);
            return Json(new { result = search });
        }
        public JsonResult GetZlecenia()
        {
            var zlecenia = _dbContext.Items.Select(i => i).ToList();
            return Json(zlecenia);
        }
        public IActionResult Index()
        {
            var recipies = new SelectList(_dbContext.Recipes.Select(n => n.Name).ToList());
            var opakowanie = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Container").Select(n => n.Name).ToList());
            var dekel = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Cap").Select(n => n.Name).ToList());
            var naklejka = new SelectList(_dbContext.Ingredients.Where(i => i.Use == "Label").Select(n => n.Name).ToList());
            ViewBag.Recipies = recipies;
            ViewBag.Opakowania = opakowanie;
            ViewBag.Dekel = dekel;
            ViewBag.Naklejka = naklejka;
            return View();
        }
        public JsonResult Details(int id)
        {
            var zlecenie = _dbContext.Items.Where(i => i.Id == id).Select(i => i).FirstOrDefault();
            var surowce = _dbContext.Relations.Where(i => i.RecipeId == zlecenie.RecipeId).Select(i => i.IngredientsId).ToList();
            List<Ingredient> opakowaniaList = new List<Ingredient>()
            {
                _dbContext.Ingredients.Where(n => n.Name == zlecenie.Opakowanie).Select(o => o).FirstOrDefault(),
                _dbContext.Ingredients.Where(n => n.Name == zlecenie.PokrywaNekrętka).Select(o => o).FirstOrDefault(),
                _dbContext.Ingredients.Where(n => n.Name == zlecenie.Naklejka).Select(o => o).FirstOrDefault()
            };

            List<Tuple<int, string, double>> ingredients = new List<Tuple<int, string, double>>();
            foreach (var elem in surowce)
            {
                var counts = _dbContext.ItemsCount.Where(i => i.IngredientId == elem).Select(i => i.IngredientCount).FirstOrDefault();
                var ingredient = _dbContext.Ingredients.Where(i => i.Id == elem).Select(i => i).FirstOrDefault();
                ingredients.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));
            }
            return Json(new { items = ingredients,details = zlecenie, opakowania = opakowaniaList });
        }
        public async Task<IActionResult> NoweZlecenie(Item model)
        {
            
            Item item = model;
            item.RecipeId = _dbContext.Recipes.Where(n => n.Name == model.RecipesName).Select(i => i.Id).FirstOrDefault();

            var capasity = _dbContext.Ingredients.Where(n => n.Name == model.Opakowanie).Select(i => i.Capacity).FirstOrDefault();
            item.IlośćOpakowań = Convert.ToInt32(model.Count/capasity);
            item.IlośćNaklejek = item.IlośćOpakowań;
            item.IlośćPokrywNekrętek = item.IlośćOpakowań;

            await _dbContext.Items.AddAsync(item);
            _dbContext.SaveChanges();
           
            
            var surowce = _dbContext.Relations.Where(i => i.RecipeId == item.RecipeId).Select(i => i).ToList();
            List<ItemsCount> ingredientsCount = new List<ItemsCount>();
            foreach(var id in surowce)
            {
                var surowiec = new ItemsCount();
                surowiec.IngredientId = id.IngredientsId;
                surowiec.ItemId = item.Id;
                surowiec.IngredientCount = DefaultRecipies.Count(model.Count, id.Amount);
                ingredientsCount.Add(surowiec);
            }
            
            await _dbContext.ItemsCount.AddRangeAsync(ingredientsCount);         
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Test()
        {
            //var ingredients = _dbContext.Ingredients.Select(i => i.Name).ToList();
            return View();
        }
       
        [HttpPost]
        public IActionResult PostMethod([FromBody]IEnumerable<Order> orders)
        {
            if(orders != null)
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
                        OrdersForWarehouse defaultForOrders =new OrdersForWarehouse();
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
        //[HttpPost]
        public IActionResult NoveZamówienie(Order order) {
            foreach(var item in order.Ingredient)
            {
                NewOrder newOrder = new NewOrder();
                newOrder.ItemId = _dbContext.Items.Select(i => i.Id).FirstOrDefault();
                newOrder.MachineId = 1;
                newOrder.ProcessId = 1;
                newOrder.Status = "Free";

                //DefaultForOrders defaultForOrders = new OrdersForWarehouse();
                //defaultForOrders = newOrder;
                _dbContext.NewOrders.Add(newOrder);
                //_dbContext.OrdersForWarehouse.Add(defaultForOrders);
                _dbContext.SaveChanges();
            }

            return View();
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
                try {
                    var counts = _dbContext.ItemsCount.Where(i => i.IngredientId == ingredient.Id).Select(i => i.IngredientCount).FirstOrDefault();
                    ingredients.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));

                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }

            };
              if(ingredients.Count() != 0) 
                {
                    return Json(new { details = zlecenie, opakowania = opakowaniaList, items = ingredients, num = 1, onOrders = orders });
                }
              else if(opakowaniaList.Where(i => i != null).ToList().Count() != 0)
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
            return Json(new { details = zlecenie, num = 0});
        }
        public JsonResult GetCountByNum(int numerMaterialu, int count, int numerZlecenia)
        {
            var result = _dbContext.NewOrders.Find(numerMaterialu);
            if(result != null)
            {
                var ingredient = _dbContext.Ingredients.Where(n => n.MaterialNumber == numerMaterialu).Select(i => i).FirstOrDefault();
                var orderedNum = _dbContext.NewOrders.Where(n => n.IngredientNumber == numerMaterialu && n.ItemId == numerZlecenia).Select(c => c.Count).Sum();
                if(ingredient.SectionName == "Składniki")
                {
                    var ingredienCount = _dbContext.ItemsCount.Where(i => i.IngredientId == ingredient.Id && i.ItemId == numerZlecenia).Select(c => c.IngredientCount).FirstOrDefault();
                    if((count / orderedNum)*100 <= 20)
                    {

                    }
                }
            }
            return Json(null);
        }
        //[HttpPost]
        public IActionResult SetByNumberInput ([FromBody]IEnumerable<Order> orders)
        {
            if(orders != null)
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
               

                return Json(new { message = newOrder});
                    }
                catch(Exception e)
                    {
                e.Message.ToString();
                    }
            }
          
            return Json(new { message = "Error!" });
        }
        public JsonResult GetOrders()
        {
            var nOrders = _dbContext.NewOrders.Select(o => o).ToList();
            List<string> ingredients = new List<string>();

            foreach(var item in nOrders)
            {
                ingredients.Add(_dbContext.Ingredients.Where(n => n.MaterialNumber == item.IngredientNumber).Select(n => n.Name).FirstOrDefault());
            }

            List<Tuple<int, string, double, string, int>> orders = new List<Tuple<int, string, double, string, int>>();

            for(var i = 0 ; i< nOrders.Count(); i++)
            {
                orders.Add(new Tuple<int, string, double, string, int> (nOrders.Select(n => n.IngredientNumber).ToList()[i], ingredients[i], nOrders.Select(n => n.Count).ToList()[i], nOrders.Select(s => s.Status).ToList()[i], nOrders.Select(i => i.Id).ToList()[i]));
            }
          
            return Json(orders);
        }
        public JsonResult GetOrdersForWarehouse()
        {
            var nOrders = _dbContext.OrdersForWarehouse.Select(o => o).ToList();
            List<string> ingredients = new List<string>();

            foreach (var item in nOrders)
            {
                ingredients.Add(_dbContext.Ingredients.Where(n => n.MaterialNumber == item.IngredientNumber).Select(n => n.Name).FirstOrDefault());
            }

            List<Tuple<int, string, double, string, int, string, string>> orders = new List<Tuple<int, string, double, string, int, string,string>>();

            foreach(var item in nOrders)
            {
                //orders.Add(new Tuple<int, string, double, string, int, string, string>(item.I, ingredients[i], nOrders.Select(n => n.Count).ToList()[i], nOrders.Select(s => s.Status).ToList()[i], nOrders.Select(i => i.Id).ToList()[i], nOrders.Select));
                  orders.Add(new Tuple<int, string, double, string, int, string, string>(item.IngredientNumber,_dbContext.Ingredients.Where(i => i.MaterialNumber == item.IngredientNumber).Select(n => n.Name).FirstOrDefault(),item.Count,item.Status,item.Id, item.DataZamówienia, item.DataRealizacji));

            }

            return Json(orders);
        }
        public async Task<IActionResult> ZmianaStatusu(int id)
        {

            if (id != 0)
            {

                try
                {
                    var status = _dbContext.NewOrders.Where(i => i.Id == id).Select(o => o).FirstOrDefault();
                    status.Status = "Dostarczone";

                    _dbContext.NewOrders.Update(status);
                    var order = _dbContext.OrdersForWarehouse.Where(i => i.Id == status.Id).Select(o => o).FirstOrDefault();
                    order.Status = status.Status;
                    order.DataRealizacji = DateTime.Now.ToString();
                    _dbContext.OrdersForWarehouse.Update(order);
                    await _dbContext.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception e)
                {
                    return NotFound();
                }

            }
            return NotFound();
        }

        public JsonResult GetStatus(int id)
        {
            var status = _dbContext.NewOrders.Where(i => i.Id == id).Select(s => s.Status).FirstOrDefault();
            return Json(status);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if(id != 0)
            {
                _dbContext.OrdersForWarehouse.Remove(_dbContext.OrdersForWarehouse.Where(i => i.Id == id).FirstOrDefault());
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            return NotFound();

        }
        public IActionResult Warehouse()
        {
            return View();
        }
        public JsonResult GetСount(string id)
        {
            var request = id.Split(',');
            var count = _dbContext.NewOrders.Where(n => n.IngredientNumber == Int32.Parse(request[0])).Select(c => c.Count).FirstOrDefault();
            try
            {
                if(Int32.Parse(request[1]) != 0)
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
            catch(Exception e)
            {
                return Json(3);
            }

        }
    }
}
