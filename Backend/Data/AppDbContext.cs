using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet<T>-Eigenschaften für die Entities hier hinzufügen, z.B.:
    // public DbSet<Item> Items => Set<Item>();
}
