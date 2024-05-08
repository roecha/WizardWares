using Microsoft.EntityFrameworkCore;
using WizardWares.Models;


namespace WizardWares.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Potions", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Spell Books", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Wands", DisplayOrder = 3 }

            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "inferior health potion",
                    Description = "Drink this potion to recover 50 health.",
                    PriceMoney = 15,
                    PriceObject = "A piece of hidden lore",
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Name = "inferior stamina potion",
                    Description = "Drink this potion to recover 50 stamina.",
                    PriceMoney = 15,
                    PriceObject = "A piece of hidden lore",
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Name = "inferior mana potion",
                    Description = "Drink this potion to recover 50 mana.",
                    PriceMoney = 15,
                    PriceObject = "A piece of hidden lore",
                    CategoryId = 1,
                    ImageUrl = ""
                }
            );
        }
    }
}
