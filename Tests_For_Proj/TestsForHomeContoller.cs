using Ingredients_order.Controllers;
using Ingredients_order.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Moq;

namespace Tests_For_Proj
{
    public class TestsForHomeContoller
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly SignInManager<UsersIdentity> _signInManager;
        private readonly UserManager<UsersIdentity> _userManager;
        private readonly HomeController _homeController;

        public TestsForHomeContoller()
        {
            var context = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "IngredientOrder")
           .Options;
            _applicationDb = new ApplicationDbContext(context);
            _homeController = new HomeController(_applicationDb, _signInManager, _userManager);
        }

        [Fact]
        public void TestGetZlecenie()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "IngredientOrder")
                 .Options;

            using (var dbContext = new ApplicationDbContext(dbContextOptions))
            {
                dbContext.Items.Add(new Item { NrZlecenia = 1, Id = 1 });
                dbContext.SaveChanges();
                var result = _homeController.GetZlecenie(1) as JsonResult;

                Assert.NotNull(result);
                var searchResult = result.Value.GetType().GetProperty("result").GetValue(result.Value, null);
                Assert.True((bool)searchResult);

                dbContext.Database.EnsureDeleted();
            }

        }

        [Fact]
        public void TestGetZlecenia()
        {
            _applicationDb.Items.AddRange(
                new Item { NrZlecenia = 1, Id = 1 },
                new Item { NrZlecenia = 2, Id = 2 },
                new Item { NrZlecenia = 3, Id = 3 }
            );
            _applicationDb.SaveChanges();
            var controller = new HomeController(_applicationDb, _signInManager, _userManager);

            // Act
            var result = controller.GetZlecenia() as JsonResult;

            // Assert
            Assert.NotNull(result);
            var resultList = result.Value as List<Item>;
            Assert.NotNull(resultList);
            Assert.Equal(_applicationDb.Items.CountAsync().Result, resultList.Count);
            Assert.Equal(_applicationDb.Items.ToListAsync().Result, resultList);

            _applicationDb.Database.EnsureDeleted();
        }
        [Fact]
        public void TestDetails()
        {
            var item = new Item
            {
                Id = 1,
                RecipeId = 1,
                NrZlecenia = 123
            };
            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "Test ingredient"
            };
            var relation = new Relation
            {
                RecipeId = item.RecipeId,
                IngredientsId = ingredient.Id
            };
            _applicationDb.Items.Add(item);
            _applicationDb.Ingredients.Add(ingredient);
            _applicationDb.Relations.Add(relation);
            _applicationDb.SaveChanges();

            // Act
            var result = _homeController.Details(item.Id) as JsonResult;

            // Assert
            Assert.NotNull(result);
            _applicationDb.Database.EnsureDeleted();

        }
        [Fact]
        public async Task NoweZlecenie_CreatesNewItem_WhenModelIsNotNull()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "IngredientOrder")
                .Options;

            using (var dbContext = new ApplicationDbContext(dbContextOptions))
            {
                dbContext.Recipes.Add(new Recipe { Name = "RecipeName", Id = 1 });
                dbContext.Ingredients.Add(new Ingredient { Name = "Packaging", Capacity = 100, Id = 1 });
                dbContext.SaveChanges();
            }

            using (var dbContext = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new HomeController(dbContext, _signInManager, _userManager);

                var model = new Item
                {
                    RecipesName = "RecipeName",
                    Opakowanie = "Packaging"
                };

                // Act
                var result = await controller.NoweZlecenie(model);

                // Assert
                var itemCount = dbContext.Items.CountAsync();
                Assert.Equal(1, itemCount.Result);

                var newItem = dbContext.Items.FirstOrDefaultAsync();
                Assert.NotNull(newItem);
                _applicationDb.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task Delete_ShouldRemoveItem_WhenIdIsValid()
        {
            // Arrange
            var testId = 1;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "IngredientOrder")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            var controller = new HomeController(dbContext, _signInManager, _userManager);

            // Act
            var result = await controller.Delete(testId);
            var actual = await dbContext.Items.FindAsync(testId);

            // Assert
            Assert.NotNull(result);

        }
        [Fact]
        public void EditOrder_ReturnsJsonResult_WhenValidId()
        {
            // Arrange
            int validId = 5;
            _applicationDb.Items.AddRange(
                new Item { NrZlecenia = 1, Id = 5 },
                new Item { NrZlecenia = 2, Id = 2 },
                new Item { NrZlecenia = 3, Id = 3 }
            );
            _applicationDb.SaveChanges();
            var controller = new HomeController(_applicationDb, _signInManager, _userManager);

            // Act
            var result = controller.EditOrder(validId) as JsonResult;

            // Assert
            Assert.NotNull(result);
            _applicationDb.Database.EnsureDeleted();

        }
        [Fact]
        public async Task EditedOrder_UpdatesIngredientsCountAndRedirectsToIndex()
        {
            // Arrange
            var model = new Item() { Id = 3, NrZlecenia = 4 };
            var controller = new HomeController(_applicationDb, _signInManager, _userManager);
            // Act
            var result = await controller.EditedOrder(model) as RedirectToActionResult;

            // Assert
            Assert.Equal("Index", result.ActionName);
        }

    }
}
