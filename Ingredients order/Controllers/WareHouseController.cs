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
    public class WareHouseController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public WareHouseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult WareHouse()
        {
            return View();
        }
        [Authorize]
        public IActionResult WareHouseCount()
        {
            var materials = _dbContext.Ingredients.Select(i => i).ToList();
            ViewBag.MaterialNames = new SelectList(materials.Where(n => n.Name != "Woda").Select(n => new {n.Id, n.Name}).ToList(), "Id", "Name");
            return View();
        }
        [Authorize]
        public JsonResult GetPallets()
        {
            try
            {
                var orders = _dbContext.PalettModel.Select(p => new { p.Id, p.Ilość, p.Localization, p.Material.Name, p.Material.MaterialNumber, p.PalletNumber }).ToList();
                return Json(orders);
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return Json(null);
        }
        [Authorize]
        public JsonResult GetOrdersForWarehouse()
        {
            try
            {
                var nOrders = _dbContext.OrdersForWarehouse.Select(o => o).ToList();
                List<string> ingredients = new List<string>();
                List<PalettModel> pallets = new List<PalettModel>();

                foreach (var item in nOrders)
                {
                    ingredients.Add(_dbContext.Ingredients.Where(n => n.MaterialNumber == item.IngredientNumber).Select(n => n.Name).FirstOrDefault());
                    pallets.Add(_dbContext.PalettModel.Where(n => n.PalletNumber == item.Palett).Select(p => p).First());
                }

                List<Tuple<TupleModel>> orders = new List<Tuple<TupleModel>>();
                for (var i = 0; i < nOrders.Count; i++)
                {
                    TupleModel tuple = new TupleModel
                    {
                        IngredientNumber = nOrders[i].IngredientNumber,
                        IngredientName = _dbContext.Ingredients.Where(n => n.MaterialNumber == nOrders[i].IngredientNumber).Select(n => n.Name).FirstOrDefault(),
                        Count = pallets.Where(o => o.OrdersForWarehouseId == nOrders[i].Id).First().Ilość,
                        Status = pallets.Where(o => o.OrdersForWarehouseId == nOrders[i].Id).First().Status,
                        Id = pallets.Where(o => o.OrdersForWarehouseId == nOrders[i].Id).First().Id,
                        DataZamówienia = nOrders[i].DataZamówienia,
                        DataRealizacji = nOrders[i].DataRealizacji,
                        NumerPalety = pallets.Where(o => o.OrdersForWarehouseId == nOrders[i].Id).First().PalletNumber
                    };
                    orders.Add(new Tuple<TupleModel>(tuple));

                }

                return Json(orders.Select(e => e.Item1).ToList());
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return Json(null);

        }
        [Authorize]
        public JsonResult CheckPalletNumber(string number)
        {
            if(number != null)
            {
                try
                {
                    var check = _dbContext.PalettModel.Select(n => n.PalletNumber).Contains(long.Parse(number));
                    return Json(new { result = check });
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return null;

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PalletAdding(PalettModel pallet)
        {
            if(pallet != null)
            {
                try
                {
                    var ingredient = _dbContext.Ingredients.Where(n => n.MaterialNumber == pallet.Material.MaterialNumber).Select(i => i).First();
                    PalettModel newPallet = new PalettModel
                    {
                        Ilość = pallet.Ilość,
                        Localization = "Magazyn",
                        Material = ingredient,
                        PalletNumber = pallet.PalletNumber,
                        Status = "Free to use",


                    };
                    _dbContext.PalettModel.Add(newPallet);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("WareHouseCount");
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }

            }
            return RedirectToAction("WareHouseCount");

        }
        [Authorize]
        public JsonResult GetMaterial(string materilName)
        {
            if(materilName != null)
            {
                try
                {
                    var materialNumber = _dbContext.Ingredients.Where(n => n.Name == materilName).Select(n => n.MaterialNumber).SingleOrDefault();
                    return Json(new { number = materialNumber });
                }
                catch(Exception e)
                {
                    e.Message.ToString();
                }
            }
            return Json(null);
        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> DeleteOrder(long palletNumber)
        {
            if(palletNumber != 0)
            {
                try
                {
                    var order = _dbContext.OrdersForWarehouse.Where(p => p.Palett == palletNumber).First();
                    var pallet = _dbContext.PalettModel.Where(p => p.PalletNumber == palletNumber).Select(p => p).First();
                    pallet.OrdersForWarehouseId = null;
                    pallet.OrdersForWarehouse = null;
                    _dbContext.PalettModel.Update(pallet);
                    _dbContext.OrdersForWarehouse.Remove(order);
                    await _dbContext.SaveChangesAsync();

                    return Json("The order has been deleted!");
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            return Json("Some error occured!");

        }
        [Authorize]
        [HttpGet]
        public JsonResult GetStatus(string palletNumber)
        {
            
            if(palletNumber != null)
            {
                try
                {
                    var palletStatus = _dbContext.PalettModel.Where(n => n.PalletNumber == long.Parse(palletNumber)).Select(i => i.Status).First();
                    return Json(palletStatus);
                }
                catch(Exception e)
                {
                    e.Message.ToString();
                }
            }
            return Json(null);
        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> ZmianaStatusu(string  palletNumber)
        {
            if(palletNumber != null)
            {
                try
                {
                    var pallet = _dbContext.PalettModel.Where(n => n.PalletNumber == long.Parse(palletNumber)).Select(p => p).First();
                    pallet.Status = "Dostarczone";
                    _dbContext.PalettModel.Update(pallet);
                    var order = _dbContext.OrdersForWarehouse.Where(i => i.Palett == pallet.PalletNumber).Select(o => o).First();
                    order.DataRealizacji = DateTime.Now.ToString();
                    _dbContext.OrdersForWarehouse.Update(order);

                    await _dbContext.SaveChangesAsync();

                    return Json("Status has been changed!");
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
    
            }
            return Json("Some error occured!");
        }


    }
}
