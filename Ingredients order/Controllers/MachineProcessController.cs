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
        public IActionResult ProcessMachineSelectingTest()
        {
            return View();
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

                List<Tuple<int, string, double, string, int, long>> orders = new List<Tuple<int, string, double, string, int, long>>();

                for (var i = 0; i < nOrders.Count(); i++)
                {
                    orders.Add(new Tuple<int, string, double, string, int, long>(nOrders.Select(n => n.IngredientNumber).ToList()[i], ingredients[i], nOrders.Select(n => n.Count).ToList()[i], nOrders.Select(s => s.Status).ToList()[i], nOrders.Select(i => i.Id).ToList()[i], nOrders.Select(i => i.Palett).ToList()[i]));
                }

                return Json(orders);
            }
            catch (Exception e)
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
        public JsonResult GetIngredients(int zlecenieNumber)
        {
            //var zlecenie = _dbContext.Items.Select(i => i).FirstOrDefault();
            var zlecenie = _dbContext.Items.Where(i => i.NrZlecenia == zlecenieNumber).Select(i => i).FirstOrDefault();

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
        public IActionResult Test()
        {
            return View();
        }
        public JsonResult GetСount(string id)
        {
            if (id != null)
            {
                try
                {
                    var request = id.Split(',');
                    var count = _dbContext.NewOrders.Where(n => n.IngredientNumber == Int32.Parse(request[0])).Select(c => c.Count).FirstOrDefault();

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
                    e.Message.ToString();
                    return Json(3);
                }
            }

            return null;
        }
        /*
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
        */
        public JsonResult TestPallet()
        {
            // var order = new Order { Count = 500 };
            // var pallets = _dbContext.PalettModel.Where(m => m.Material.Id == 1).Select(n => n).ToList();
            //  List<PalettModel> newPalletts = DefaultRecipies.ListOfPallets(pallets, 500);
            // return Json(newPalletts);
            //var zamówienia = _dbContext.NewOrders.Select(n => n).ToList();
            //return Json(zamówienia);
            try
            {
                var nOrders = _dbContext.NewOrders.Select(o => o).ToList();
                List<string> ingredients = new List<string>();

                foreach (var item in nOrders)
                {
                    ingredients.Add(_dbContext.Ingredients.Where(n => n.MaterialNumber == item.IngredientNumber).Select(n => n.Name).FirstOrDefault());
                }

                List<Tuple<int, string, double, string, int, long>> orders = new List<Tuple<int, string, double, string, int, long>>();

                for (var i = 0; i < nOrders.Count(); i++)
                {
                    orders.Add(new Tuple<int, string, double, string, int, long>(nOrders.Select(n => n.IngredientNumber).ToList()[i], ingredients[i], nOrders.Select(n => n.Count).ToList()[i], nOrders.Select(s => s.Status).ToList()[i], nOrders.Select(i => i.Id).ToList()[i], nOrders.Select(i => i.Palett).ToList()[i]));
                }

                return Json(orders);
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return Json(null);
        }
        public async Task<IActionResult> PostMethod([FromBody] IEnumerable<Order> orders)
        {
            if (orders != null)
            {
                try
                {
                    foreach (var order in orders)
                    {
                        var pallets = _dbContext.PalettModel.Where(n => n.Material.MaterialNumber == Int32.Parse(order.Ingredient) && n.Status == "Free to use").Select(n => n).ToList();
                        var palletsTest = _dbContext.PalettModel.Select(n => n).ToList();
                        var machine = _dbContext.Machines.Where(i => i.Id == order.MachineId).Select(n => n.Name).Single();
                        var process = _dbContext.Processes.Where(i => i.Id == order.ProcessId).Select(n => n.Name).Single();
                        var ingredient = _dbContext.Ingredients.Where(i => i.MaterialNumber == Int32.Parse(order.Ingredient)).Select(i => i).FirstOrDefault();
                        double count = 0;
                        if (ingredient.SectionName == "Składniki")
                        {
                            count = _dbContext.ItemsCount.Where(i => i.IngredientId == ingredient.Id).Select(i => i.IngredientCount).FirstOrDefault();
                        }
                        else
                        {
                            count = _dbContext.Items.Where(i => i.NrZlecenia == order.ItemId).Select(o => o.IlośćOpakowań).FirstOrDefault();

                        }
                        List<PalettModel> newPalletts = new List<PalettModel>();

                        if(Convert.ToInt32(order.Count) == 0)
                        {
                            newPalletts = DefaultRecipies.ListOfPallets(pallets, Convert.ToInt32(count));
                        }
                        else
                        {
                            newPalletts = DefaultRecipies.ListOfPallets(pallets, Convert.ToInt32(order.Count));
                        }
                        foreach (var pallet in newPalletts)
                           {
                                NewOrder newOrder = new NewOrder();
                                newOrder.IngredientNumber = Int32.Parse(order.Ingredient);
                                newOrder.ItemId = order.ItemId;
                                newOrder.MachineId = order.MachineId;
                                newOrder.ProcessId = order.ProcessId;
                                newOrder.Status = "W trakcie realizacji";
                                newOrder.Palett = pallet.PalletNumber;
                                newOrder.Count = pallet.Ilość;
     
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
                            defaultForOrders.Palett = newOrder.Palett;

                            _dbContext.OrdersForWarehouse.Add(defaultForOrders);

                            pallet.Status = "Currently using";
                            pallet.Localization = process + " "+ machine;
                            _dbContext.PalettModel.Update(pallet);
                        }

                        await _dbContext.SaveChangesAsync();
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
        /*
        public IActionResult SetByNumberInput([FromBody] IEnumerable<Order> orders)
        {
            if (orders != null)
            {
                try
                {
                    foreach (var order in orders)
                    {
                        var pallets = _dbContext.PalettModel.Where(n => n.Material.MaterialNumber == Int32.Parse(order.Ingredient)).Select(n => n).ToList();
                        var palletsTest = _dbContext.PalettModel.Select(n => n).ToList();
                        var machine = _dbContext.Machines.Where(i => i.Id == order.MachineId).Select(n => n.Name).Single();
                        var process = _dbContext.Processes.Where(i => i.Id == order.ProcessId).Select(n => n.Name).Single();
                        var ingredient = _dbContext.Ingredients.Where(i => i.MaterialNumber == Int32.Parse(order.Ingredient)).Select(i => i).FirstOrDefault();
                        
                        List<PalettModel> newPalletts = DefaultRecipies.ListOfPallets(pallets, Convert.ToInt32(order.Count));
                        //List<PalettModel> newPalletts = DefaultRecipies.ListOfPallets(pallets, Convert.ToInt32(count));
                        foreach (var pallet in newPalletts)
                        {
                            NewOrder newOrder = new NewOrder();
                            newOrder.IngredientNumber = Int32.Parse(order.Ingredient);
                            newOrder.ItemId = order.ItemId;
                            newOrder.MachineId = order.MachineId;
                            newOrder.ProcessId = order.ProcessId;
                            newOrder.Status = "W trakcie realizacji";
                            newOrder.Palett = pallet.PalletNumber;
                            newOrder.Count = pallet.Ilość;

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
                            defaultForOrders.Palett = newOrder.Palett;

                            _dbContext.OrdersForWarehouse.Add(defaultForOrders);

                            pallet.Status = "Currently using";
                            pallet.Localization = process + " " + machine;
                            _dbContext.PalettModel.Update(pallet);
                        }

                         _dbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
                return Ok();
            }

            return Json(new { message = "Error!" });
            /*
            if (orders != null)
            {
                try
                {
                    NewOrder newOrder = new NewOrder();

                    foreach (var order in orders)
                    {
                        var machine = _dbContext.Machines.Where(i => i.Id == order.MachineId).Select(n => n.Name).Single();
                        var process = _dbContext.Processes.Where(i => i.Id == order.ProcessId).Select(n => n.Name).Single();
                        var pallets = _dbContext.PalettModel.Where(n => n.Material.MaterialNumber == Int32.Parse(order.Ingredient)).Select(n => n).ToList();
                        List<PalettModel> newPalletts = DefaultRecipies.ListOfPallets(pallets, Convert.ToInt32(order.Count));
                        foreach (var pallet in newPalletts)
                        {
                            newOrder.IngredientNumber = Int32.Parse(order.Ingredient);
                            newOrder.ItemId = order.ItemId;
                            newOrder.MachineId = order.MachineId;
                            newOrder.ProcessId = order.ProcessId;
                            newOrder.Status = "W trakcie realizacji";
                            newOrder.Count = order.Count;

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

                            pallet.Status = "Currently using";
                            pallet.Localization = process + " " + machine;
                            _dbContext.PalettModel.Update(pallet);
                        }
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
        */
        public JsonResult CheckMaterialNumber(string number)
        {
            try
            {
                var check = _dbContext.Ingredients.Select(n => n.MaterialNumber).Contains(Int32.Parse(number));
                return Json(new { result = check });
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;

        }
        public IActionResult PalletReturn (int palletNumber)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var pallet = _dbContext.PalettModel.Where(n => n.PalletNumber == palletNumber).Select(p => p).First();
                    pallet.Localization = "Magazyn";
                    pallet.Status = "Free to use";

                    _dbContext.PalettModel.Update(pallet);
                    _dbContext.SaveChanges();
                }
                catch(Exception e)
                {
                    e.Message.ToString();
                }
                return Ok();
            }
            return NotFound();
        }

       
        public JsonResult Consumption(int palletNumber)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var pallet = _dbContext.PalettModel.Where(n => n.PalletNumber == palletNumber).Select(p => p).FirstOrDefault();
                    var material = _dbContext.Ingredients.Where(i => i.Id == pallet.IngredientId).Select(m => m).First();
                    return Json(new { nrPalety = pallet.PalletNumber, nrMaterialu = material.MaterialNumber, materialName = material.Name, lokalizacja = pallet.Localization, count = pallet.Ilość });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return Json(null);

        }

        [HttpPost]
        public async Task<IActionResult> PalletUpdate(int palettNumber, int count)
        {
            if(palettNumber != 0)
            {
                var pallet = _dbContext.PalettModel.Where(n => n.PalletNumber == palettNumber).Select(p => p).First();
                pallet.Ilość = count;
                try
                {
                    if(count == 0)
                    {
                        _dbContext.PalettModel.Remove(pallet);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        _dbContext.PalettModel.Update(pallet);
                        await _dbContext.SaveChangesAsync();
                    }
                    return RedirectToAction("ProcessMachineSelecting");
                }catch(Exception e)
                {
                    e.Message.ToString();
                }
            }
            return NotFound();
        }

    }
}
