using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Opakowania> Opakowania { get; set; }
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
