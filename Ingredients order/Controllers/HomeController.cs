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
        public JsonResult Details(int id)
        {
            var zlecenie = _dbContext.Items.Where(i => i.Id == id).Select(i => i).FirstOrDefault();
            var surowce = _dbContext.Relations.Where(i => i.RecipeId == zlecenie.RecipeId).Select(i => i.IngredientsId).ToList();
            List<Opakowania> opakowania = new List<Opakowania>()
            {
                _dbContext.Opakowania.Where(n => n.Name == zlecenie.Opakowanie).Select(o => o).FirstOrDefault(),
                _dbContext.Opakowania.Where(n => n.Name == zlecenie.PokrywaNekrętka).Select(o => o).FirstOrDefault(),
                _dbContext.Opakowania.Where(n => n.Name == zlecenie.Opakowanie).Select(o => o).FirstOrDefault()
            };

            List<Tuple<int, string, double>> ingredients = new List<Tuple<int, string, double>>();
            foreach (var elem in surowce)
            {
                var counts = _dbContext.ItemsCount.Where(i => i.IngredientId == elem).Select(i => i.IngredientCount).FirstOrDefault();
                var ingredient = _dbContext.Ingredients.Where(i => i.Id == elem).Select(i => i).FirstOrDefault();
                ingredients.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));
            }
            /*
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach(var elem in surowce)
            {
                var ingredient = _dbContext.Ingredients.Where(i => i.Id == elem).Select(i => i).FirstOrDefault();
                ingredients.Add(ingredient);
            }
            var counts = _dbContext.ItemsCount.Where(i => i.ItemId == id).Select(i => i).ToList();

            return Json(new {zlecenie, ingredients, opakowania, counts});
            */
            return Json(new { items = ingredients,details = zlecenie });
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
            _dbContext.ItemsCount.AddRange(ingredientsCount);
            
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
