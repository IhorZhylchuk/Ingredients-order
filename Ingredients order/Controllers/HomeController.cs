using Ingredients_order.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<UsersIdentity> _signInManager;
        private readonly UserManager<UsersIdentity> _userManager;

        public HomeController(ApplicationDbContext dbContext, SignInManager<UsersIdentity> signInManager, UserManager<UsersIdentity> user)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = user;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel login = new LoginViewModel();
            var user = _userManager.FindByEmailAsync(login.Email);
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ViewBag.Result = "Error";
                ModelState.AddModelError("", "User was not found. Chek your email or password!");
                return View();


            }
                catch (Exception ex)
                {
                ex.Message.ToString();
                }
            }
            return View();
        }

        [Authorize]
        public JsonResult GetZlecenie(int numerZlecenia)
        {
            var search = _dbContext.Items.Select(i => i.NrZlecenia).ToList().Any(i => i == numerZlecenia);
            return Json(new { result = search });
        }
        [Authorize]
        public JsonResult GetZlecenia()
        {
            var zlecenia = _dbContext.Items.Select(i => i).ToList();
            return Json(zlecenia);
        }

        [Authorize]
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

        [Authorize]
        public JsonResult Details(int id)
        {
            if(id != 0)
            {
                try 
                {
                    var zlecenie = _dbContext.Items.Where(i => i.Id == id).Select(i => i).First();
                    var surowce = _dbContext.Relations.Where(i => i.RecipeId == zlecenie.RecipeId).Select(i => i.IngredientsId).ToList();
                    List<Ingredient> opakowaniaList = new List<Ingredient>()
                    {
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.Opakowanie).Select(o => o).First(),
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.PokrywaNekrętka).Select(o => o).First(),
                        _dbContext.Ingredients.Where(n => n.Name == zlecenie.Naklejka).Select(o => o).First()
                    };
            
                    List<Tuple<int, string, double>> ingredients = new List<Tuple<int, string, double>>();
                    foreach (var elem in surowce)
                    {
                        var counts = _dbContext.ItemsCount.Where(i => i.IngredientId == elem).Select(i => i.IngredientCount).First();
                        var ingredient = _dbContext.Ingredients.Where(i => i.Id == elem).Select(i => i).First();
                        ingredients.Add(new Tuple<int, string, double>(ingredient.MaterialNumber, ingredient.Name, counts));
                    }
            return Json(new { items = ingredients, details = zlecenie, opakowania = opakowaniaList });
               }
              catch (Exception e)
                {
                 e.Message.ToString();
                }
              }
            return Json(null);
        }

        [Authorize]
        public async Task<IActionResult> NoweZlecenie(Item model)
        {
            if(model != null)
            {
                try
                {
                    Item item = model;
                    item.RecipeId = _dbContext.Recipes.Where(n => n.Name == model.RecipesName).Select(i => i.Id).FirstOrDefault();

                    var capasity = _dbContext.Ingredients.Where(n => n.Name == model.Opakowanie).Select(i => i.Capacity).FirstOrDefault();
                    item.IlośćOpakowań = Convert.ToInt32(model.Count / capasity);
                    item.IlośćNaklejek = item.IlośćOpakowań;
                    item.IlośćPokrywNekrętek = item.IlośćOpakowań;

                    await _dbContext.Items.AddAsync(item);
                    _dbContext.SaveChanges();


                    var surowce = _dbContext.Relations.Where(i => i.RecipeId == item.RecipeId).Select(i => i).ToList();
                    List<ItemsCount> ingredientsCount = new List<ItemsCount>();
                    foreach (var id in surowce)
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
                catch(Exception e)
                {
                    e.Message.ToString();
                }
            }
            return NotFound();
        }

        [Authorize]
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

        [Authorize]
        public JsonResult GetCountByNum(int numerMaterialu, int count, int numerZlecenia)
        {
            var result = _dbContext.NewOrders.Find(numerMaterialu);
            if(result != null)
            {
                var ingredient = _dbContext.Ingredients.Where(n => n.MaterialNumber == numerMaterialu).Select(i => i).FirstOrDefault();
                var orderedNum = _dbContext.NewOrders.Where(n => n.IngredientNumber == numerMaterialu && n.ItemId == numerZlecenia).Select(c => c.Paletts.Select(c => c.Ilość).First()).Sum();
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

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                try
                {
                    var result = _dbContext.Items.Where(b => b.Id == id).First();
                    _dbContext.Items.Remove(result);
                    await _dbContext.SaveChangesAsync();
                    return Json(new { success = true, message = "Removed successfully" });
                }
                catch(Exception e)
                {
                    e.Message.ToString();
                }
            }
            return NotFound();

        }

        [Authorize]
        [HttpGet]
        public JsonResult EditOrder(int? id)
        {
            if(id != null)
            {
                try
                {
                    var zlecenie = _dbContext.Items.Where(i => i.Id == id).Select(o => o).FirstOrDefault();
                    return Json(new { nrZlecenia = zlecenie.NrZlecenia, recipesName = zlecenie.RecipesName, count = zlecenie.Count,
                        opakowanie = zlecenie.Opakowanie, pokrywaNekrętka = zlecenie.PokrywaNekrętka, naklejka = zlecenie.Naklejka, id = zlecenie.Id });
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
        public async Task<IActionResult> EditedOrder(Item model)
        {
            if(model != null)
           {
             try
            {
            var item = _dbContext.Items.Where(i => i.Id == model.Id).Select(i => i).First();
            item.RecipesName = model.RecipesName;
            item.Id = model.Id;
            item.PokrywaNekrętka = model.PokrywaNekrętka;
            item.Opakowanie = model.Opakowanie;
            item.NrZlecenia = model.NrZlecenia;
            item.Naklejka = model.Naklejka;
            item.Count = model.Count;

            item.RecipeId = _dbContext.Recipes.Where(n => n.Name == model.RecipesName).Select(i => i.Id).First();

            var capasity = _dbContext.Ingredients.Where(n => n.Name == item.Opakowanie).Select(i => i.Capacity).First();
            item.IlośćOpakowań = Convert.ToInt32(model.Count / capasity);
            item.IlośćNaklejek = item.IlośćOpakowań;
            item.IlośćPokrywNekrętek = item.IlośćOpakowań;
            _dbContext.Items.Update(item);

            var surowce = _dbContext.Relations.Where(i => i.RecipeId == item.RecipeId).Select(i => i).ToList();
            var ingredients = _dbContext.ItemsCount.Where(i => i.ItemId == item.Id).Select(i => i).ToList();
            _dbContext.ItemsCount.RemoveRange(ingredients);
            List<ItemsCount> ingredientsCount = new List<ItemsCount>();
            foreach (var id in surowce)
            {
                var surowiec = new ItemsCount();
                surowiec.IngredientId = id.IngredientsId;
                surowiec.ItemId = item.Id;
                surowiec.IngredientCount = DefaultRecipies.Count(model.Count, id.Amount);
                ingredientsCount.Add(surowiec);
            }
            _dbContext.ItemsCount.UpdateRange(ingredientsCount);
            await _dbContext.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    e.Message.ToString();
               }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }
    }
}
