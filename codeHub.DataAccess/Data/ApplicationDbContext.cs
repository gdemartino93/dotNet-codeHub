using codeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace codeHub.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //seeding database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Java", Description = "Java World", DisplayOrder = 1, IsVisible = true },
                new Category() { Id = 2, Name = "C#", Description = "C# World", DisplayOrder = 1, IsVisible = true },
                new Category() { Id = 3, Name = "CSS", Description = "CSS World", DisplayOrder = 1, IsVisible = true }
                );
        }
    }
}
