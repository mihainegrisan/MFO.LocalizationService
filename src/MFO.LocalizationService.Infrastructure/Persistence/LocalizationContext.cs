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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CountryConfiguration());
    }
}