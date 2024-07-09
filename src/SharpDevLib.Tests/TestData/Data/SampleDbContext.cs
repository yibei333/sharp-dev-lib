using Microsoft.EntityFrameworkCore;

namespace SharpDevLib.Tests.TestData.Data;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Name);
        modelBuilder.Entity<UserFavorite>().HasKey(x => x.Favorite);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> User { get; set; }
    public DbSet<UserFavorite> UserFavorite { get; set; }
}
