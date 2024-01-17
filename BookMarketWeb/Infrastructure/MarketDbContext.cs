using BookMarketWeb.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookMarketWeb.Infrastructure;

public class MarketDbContext : DbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
    {
    }

    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    
    public MarketDbContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

public class MarketDbContextFactory : IDesignTimeDbContextFactory<MarketDbContext>
{
    private string EnvName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    public MarketDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{EnvName}.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MarketDbContext>();
        var connectionString = configuration.GetConnectionString(nameof(MarketDbContext));
        optionsBuilder.UseNpgsql(connectionString);

        return new MarketDbContext(optionsBuilder.Options);
    }
}