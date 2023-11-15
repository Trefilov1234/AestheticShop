using AestheticShop.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AestheticShop.Models
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTagConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
