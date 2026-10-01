using Microsoft.EntityFrameworkCore;
using ProductService.Models;
using System.Collections.Generic;

namespace ProductService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Book> Books => Set<Book>();

    public DbSet<ProductService.Models.Book> Book { get; set; } = default!;
}