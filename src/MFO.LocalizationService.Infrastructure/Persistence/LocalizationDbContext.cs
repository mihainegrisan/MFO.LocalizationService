using MFO.LocalizationService.Application.Interfaces;
using MFO.LocalizationService.Domain.Entities;
using MFO.LocalizationService.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MFO.LocalizationService.Infrastructure.Persistence;

public class LocalizationDbContext : AuditableDbContextBase
{
    public LocalizationDbContext(
        DbContextOptions<LocalizationDbContext> options,
        IDateTimeProvider dateTimeProvider,
        IUserContextProvider userContextProvider)
        : base(options, dateTimeProvider, userContextProvider)
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

    public override int SaveChanges()
    {
        ApplyAuditing();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditing();
        return base.SaveChangesAsync(cancellationToken);
    }
}