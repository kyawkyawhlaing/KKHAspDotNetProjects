using Microsoft.EntityFrameworkCore;
using NTier.Database.Entites;

namespace NTier.Database.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    internal sealed class Schemas
    {
        internal const string Default = "Default";
    }
}
