using dotnet_codeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_codeHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}
