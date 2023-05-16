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

namespace Tests_For_Proj
{
    public class TestsForMachineProcessController
    {

        [Fact]
        public void MachineList_ReturnsJsonResult_WhenValidProcessId()
        {
            // Arrange
            int validProcessId = 1;
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "IngredientOrder")
            .Options;
            using (var dbContext = new ApplicationDbContext(dbContextOptions))
            {
                var process = new ProcessModel { Id = 1, Name = "Process Area" };
                dbContext.Processes.Add(process);
                dbContext.Machines.Add(new MachineModel { Id = 1, Name = "Process machine № 1", ProcessModelId = 1 });
                dbContext.SaveChanges();

                var controller = new MachineProcessController(dbContext);
                var result = controller.MachineList(validProcessId) as JsonResult;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<JsonResult>(result);

                dbContext.Database.EnsureDeleted();
            }
    
        }
    }
}
