using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class ApplicationDbContext: IdentityDbContext<UsersIdentity, IdentityRole, string>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<OrdersForWarehouse> OrdersForWarehouse { get; set; }
        public DbSet<ItemsCount> ItemsCount { get; set; }
        public DbSet<NewOrder> NewOrders { get; set; }
        public DbSet<BinAttachmentModel> Bins { get; set; }
        public DbSet<ProcessModel> Processes { get; set; }
        public DbSet<MachineModel> Machines { get; set; }
        public DbSet<PalettModel> PalettModel { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            DefaultRecipies.SeedData(builder);
        }
    }
}
