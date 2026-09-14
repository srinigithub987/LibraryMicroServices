using CartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CartService.Data;

public class CartDbContext : DbContext
{
    public CartDbContext(
        DbContextOptions<CartDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cart> Carts => Set<Cart>();

    public DbSet<CartItem> CartItems =>
        Set<CartItem>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>()
            .HasMany(x => x.Items)
            .WithOne(x => x.Cart)
            .HasForeignKey(x => x.CartId);

        modelBuilder.Entity<CartItem>()
            .Property(x => x.UnitPrice)
            .HasPrecision(18, 2);
    }
}