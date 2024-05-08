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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Potions", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Spell Books", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Wands", DisplayOrder = 3 }

                );
        }
    }
}
