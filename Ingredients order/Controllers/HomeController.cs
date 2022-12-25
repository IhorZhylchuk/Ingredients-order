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
            var opakowanie = new SelectList(_dbContext.Opakowania.Where(i => i.Id < 4).Select(n => n.Name).ToList());
            var dekel = new SelectList(_dbContext.Opakowania.Where(i => i.Id > 3 && i.Id < 7).Select(n => n.Name).ToList());
            var naklejka = new SelectList(_dbContext.Opakowania.Where(i => i.Id > 6).Select(n => n.Name).ToList());
            ViewBag.Recipies = recipies;
            ViewBag.Opakowania = opakowanie;
            ViewBag.Dekel = dekel;
            ViewBag.Naklejka = naklejka;
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        public async Task<IActionResult> NoweZlecenie(Item model)
        {
            Item item = model;
            item.RecipeId = _dbContext.Recipes.Where(n => n.Name == model.RecipesName).Select(i => i.Id).FirstOrDefault();
            await _dbContext.Items.AddAsync(item);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      //  public JsonResult Opakowania()
      //  {
         //   var opakowania = _dbContext.Opakowania.Select(o => o.Name).ToList();
        //    return Json(opakowania);
        //}
    }
}
