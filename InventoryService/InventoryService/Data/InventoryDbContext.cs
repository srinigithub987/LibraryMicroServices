using InventoriesService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoriesService.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(
            DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Inventory> Inventories =>
            Set<Inventory>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasIndex(x => x.ProductId)
                .IsUnique();
        }
    }
}
