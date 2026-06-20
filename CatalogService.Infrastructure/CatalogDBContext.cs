using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure;

public class CatalogDBContext : DbContext
{
    public CatalogDBContext(DbContextOptions<CatalogDBContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDBContext).Assembly);
    }
}
