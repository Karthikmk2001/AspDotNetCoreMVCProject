using Microsoft.EntityFrameworkCore;

namespace ProjectActivity.Models
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
