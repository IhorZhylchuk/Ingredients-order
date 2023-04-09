﻿using Ingredients_order.Models;
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
        public IActionResult WareHouse()
        {
            return View();
        }
        public IActionResult WareHouseCount()
        {
            var materials = _dbContext.Ingredients.Select(i => i).ToList();
            ViewBag.MaterialNames = new SelectList(materials.Where(n => n.Name != "Woda").Select(n => new {n.Id, n.Name}).ToList(), "Id", "Name");
            return View();
        }
        public JsonResult GetPallets()
        {
            var pallets = _dbContext.PalettModel.Select(p => new { p.Id, p.Ilość, p.Localization, p.Material.Name, p.Material.MaterialNumber, p.PalletNumber }).ToList();
            return Json(pallets);
        }
        public JsonResult CheckPalletNumber(string number)
        {
            try
            {
                var check = _dbContext.PalettModel.Select(n => n.PalletNumber).Contains(long.Parse(number));
                return Json(new { result = check });
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return null;

        }
        [HttpPost]
        public async Task<IActionResult> PalletAdding(PalettModel pallet)
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
                    Status = "Free to use"
                };
                _dbContext.PalettModel.Add(newPallet);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("WareHouseCount");
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return RedirectToAction("WareHouseCount");

        }
        public JsonResult GetMaterial(string materilName)
        {
            var materialNumber = _dbContext.Ingredients.Where(n => n.Name == materilName).Select(n => n.MaterialNumber).SingleOrDefault();
            return Json(new { number = materialNumber });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id != 0)
            {
                _dbContext.OrdersForWarehouse.Remove(_dbContext.OrdersForWarehouse.Where(i => i.Id == id).FirstOrDefault());
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            return NotFound();

        }
    }
}
