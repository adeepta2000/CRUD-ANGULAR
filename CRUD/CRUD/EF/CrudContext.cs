using CRUD.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.EF
{
    public class CrudContext:DbContext
    {
        public CrudContext(DbContextOptions<CrudContext> options) : base(options) { }
        public DbSet<Product>Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);
        }*/
    }
}
