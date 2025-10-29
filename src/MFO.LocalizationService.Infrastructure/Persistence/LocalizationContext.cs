using MFO.LocalizationService.Domain.Entities;
using MFO.LocalizationService.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MFO.LocalizationService.Infrastructure.Persistence;

public class LocalizationContext : DbContext
{
    public LocalizationContext(DbContextOptions<LocalizationContext> options) 
        : base(options)
    {
        
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new RegionConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new ExchangeRateConfiguration());
    }
}