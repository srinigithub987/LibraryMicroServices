using Microsoft.EntityFrameworkCore;
using SampleApi.Models;
using System.Collections.Generic;

namespace SampleApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}