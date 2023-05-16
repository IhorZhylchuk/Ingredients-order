using Ingredients_order.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        public JsonResult MachineList(int processId)
        {
            try
            {
                var machines = _dbContext.Machines.Where(p => p.ProcessModelId == processId).Select(m => m).ToList();
                return Json(new { id = machines.Select(i => i.Id).ToList(), name = machines.Select(n => n.Name.Replace("\"", string.Empty).Trim()).ToList() });
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }

        }

        [Authorize]
        public JsonResult GetOrdersBySelect(int numerZlecenia, int processId, int machineId)
        {
            try
            {
                var nOrders = _dbContext.NewOrders.Where(n => n.ItemId == numerZlecenia && n.ProcessId == processId && n.MachineId == machineId).Select(o => o).ToList();
                List<string> ingredients = new List<string>();
                List<PalettModel> pallets = new List<PalettModel>();

                foreach (var item in nOrders)
                {
                    ingredients.Add(_dbContext.Ingredients.Where(n => n.MaterialNumber == item.IngredientNumber).Select(n => n.Name).FirstOrDefault());
                    pallets.AddRange(_dbContext.PalettModel.Where(o => o.NewOrderId == item.Id && o.Status != "Free to use").Select(p => p).ToList());
                }

                List<Tuple<int, string, int, string, int, long>> orders = new List<Tuple<int, string, int, string, int, long>>();

                for (var i = 0; i < nOrders.Count(); i++)
                {
                    foreach (var pallet in pallets)
                    {
                        if (pallet.NewOrderId == nOrders[i].Id)
                        {
                            orders.Add(new Tuple<int, string, int, string, int, long>(nOrders.Select(n => n.IngredientNumber).ToList()[i], ingredients[i], pallet.Ilość, pallet.Status, nOrders.Select(i => i.Id).ToList()[i], pallet.PalletNumber));
                        }
                    }
                    ////// ->>>>>>>>>     orders.Add(new Tuple<int, string, int, string, int, long>(nOrders.Select(n => n.IngredientNumber).ToList()[i], ingredients[i], nOrders.Select(n => n.Paletts.Select(c => c.Ilość).First()).ToList()[i], nOrders.Select(s => s.Status).ToList()[i], nOrders.Select(i => i.Id).ToList()[i], nOrders.Select(i => i.Paletts.Select(n => n.PalletNumber).First()).ToList()[i]));
                }

                return Json(orders);
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }

        }

        [Authorize]
        public JsonResult GetIngredients(int? zlecenieNumber)
        {
            if(zlecenieNumber != null)
            {
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
            return Json(null);
        }
        
        [Authorize]
        public JsonResult GetСount(string id)
        {
            if (id != null)
            {
                try
                {
                    var request = id.Split(',');
                    var count = _dbContext.NewOrders.Where(n => n.IngredientNumber == Int32.Parse(request[0])).Select(c => c.Paletts).First();

                    if (Int32.Parse(request[1]) != 0)
                    {
                        if ((Int32.Parse(request[1]) / count.Select(c => c.Ilość).First()) * 100 <= 20)
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

        [Authorize]
        public async Task<IActionResult> PostMethod([FromBody] IEnumerable<Order> orders)
        {
            if (orders != null)
            {
                try
                {
                    foreach (var order in orders)
                    {
                        var pallets = _dbContext.PalettModel.Where(n => n.Material.MaterialNumber == Int32.Parse(order.Ingredient) && n.Localization == "Magazyn").Select(n => n).ToList();
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
                        
                                NewOrder newOrder = new NewOrder();
                                newOrder.IngredientNumber = Int32.Parse(order.Ingredient);
                                newOrder.ItemId = order.ItemId;
                                newOrder.MachineId = order.MachineId;
                                newOrder.ProcessId = order.ProcessId;
     
                                _dbContext.NewOrders.Add(newOrder);
                            
                            foreach(var pallet in newPalletts)
                        {
                            OrdersForWarehouse defaultForOrders = new OrdersForWarehouse();
                            defaultForOrders.Id = newOrder.Id;
                            defaultForOrders.IngredientNumber = newOrder.IngredientNumber;
                            defaultForOrders.ItemId = newOrder.ItemId;
                            defaultForOrders.MachineId = newOrder.MachineId;
                            defaultForOrders.ProcessId = newOrder.ProcessId;
                            defaultForOrders.DataZamówienia = DateTime.Now.ToString();
                            defaultForOrders.Palett = pallet.PalletNumber;

                            _dbContext.OrdersForWarehouse.Add(defaultForOrders);

                            pallet.Status = "W trakcie realizacji";
                            pallet.Localization = process + " " + machine;
                            pallet.NewOrderId = newOrder.Id;
                            pallet.NewOrder = newOrder;
                            pallet.OrdersForWarehouseId = defaultForOrders.Id;
                            pallet.OrdersForWarehouse = defaultForOrders;

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

        [Authorize]
        public JsonResult CheckMaterialNumber(string number)
        {
            if(number != null)
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
            }
            
            return Json(null);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PalletReturn (long palletNumber)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var pallet = _dbContext.PalettModel.Where(n => n.PalletNumber == palletNumber).Select(p => p).First();
                    pallet.Localization = "Magazyn";
                    pallet.Status = "Free to use";

                    pallet.NewOrderId = null;
                    pallet.NewOrder = null;

                    if(pallet.OrdersForWarehouseId != null)
                    {
                        pallet.OrdersForWarehouseId = null;
                        pallet.OrdersForWarehouse = null;

                        var deliveredOrders = _dbContext.OrdersForWarehouse.Where(p => p.Palett == pallet.PalletNumber).Select(p => p).First();
                        _dbContext.OrdersForWarehouse.Remove(deliveredOrders);
                    }

                    _dbContext.PalettModel.Update(pallet);
                    await _dbContext.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    e.Message.ToString();
                }
                return Ok();
            }
            return NotFound();
        }

        [Authorize]
        public JsonResult Consumption(long palletNumber)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var pallet = _dbContext.PalettModel.Where(n => n.PalletNumber == palletNumber).Select(p => p).First();
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PalletUpdate(long palettNumber, int count)
        {
            if(palettNumber != 0)
            {
                try
                {
                    var pallet = _dbContext.PalettModel.Where(n => n.PalletNumber == palettNumber).Select(p => p).First();
                    pallet.Ilość = count;
                    if (count == 0)
                    {
                        _dbContext.PalettModel.Remove(pallet);
                    }
                    else
                    {
                        _dbContext.PalettModel.Update(pallet);
                    }

                    await _dbContext.SaveChangesAsync();
                    RedirectToAction("ProcessMachineSelecting");
                    return Ok();
                
                }catch(Exception e)
                {
                    e.Message.ToString();
                }
            }
            return NotFound();
        }

    }
}
