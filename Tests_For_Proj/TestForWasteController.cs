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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tests_For_Proj
{
   public class TestForWasteController
    {
        
        [Fact]
        public void Index_ReturnsViewWithProcessesInSelectList()
        {
            MainBinAttachment binAttachment = new MainBinAttachment();
            using (var dbContext = new ApplicationDbContext(Builder()))
            {
                dbContext.Processes.AddRange(binAttachment.Processes);
                dbContext.Machines.AddRange(binAttachment.Machines);

                dbContext.SaveChanges();

                var controller = new WasteController(dbContext);
                var result = controller.Index() as ViewResult;
                var list = new SelectList(binAttachment.Processes);

                Assert.NotNull(result);
                Assert.NotNull(result.ViewData.Values);

                dbContext.Database.EnsureDeleted();
            }
        }
        [Fact]
        public void CheckAttaching_JsonResultTest()
        {
            MainBinAttachment binAttachment = new MainBinAttachment();
            using (var dbContext = new ApplicationDbContext(Builder()))
            {
                dbContext.Processes.AddRange(binAttachment.Processes);
                dbContext.Machines.AddRange(binAttachment.Machines);

                var bin = new BinAttachmentModel()
                {
                    Id = 1,
                    BinNumber = "00-20-10-134",
                    BinStatus = "Free to use",
                    Machine = binAttachment.Machines[1],
                    MachineName = "Machine",
                    ProcessId = 1,
                    ProcessName = "Process"
                };
                dbContext.Bins.Add(bin);
                dbContext.SaveChanges();

                var controller = new WasteController(dbContext);
                var result = controller.CheckAttaching("1", "Machine") as JsonResult;

                Assert.NotNull(result);
                
                dbContext.Database.EnsureDeleted();
            }
        }
        [Fact]
        public async Task AttachDetach_Check_If_Model_Null()
        {
            using (var dbContext = new ApplicationDbContext(Builder()))
            {
                var controller = new WasteController(dbContext);
                var result = await controller.AttachDetach(null) as RedirectToActionResult;

                Assert.Equal("Index", result.ActionName);

                dbContext.Database.EnsureDeleted();
            }

        }
        public DbContextOptions<ApplicationDbContext> Builder()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: "IngredientOrder")
             .Options;
            return dbContextOptions;
        }
    }
}
