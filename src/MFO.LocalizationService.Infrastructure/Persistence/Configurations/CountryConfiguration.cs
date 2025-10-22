using MFO.LocalizationService.Domain.Common;
using MFO.LocalizationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFO.LocalizationService.Infrastructure.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.CountryId);

        builder.Property(x => x.Iso2Code)
            .IsRequired()
            .HasMaxLength(ValidationConstants.Iso2CodeLength);

        builder.Property(x => x.Iso3Code)
            .IsRequired()
            .HasMaxLength(ValidationConstants.Iso3CodeLength);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(ValidationConstants.CountryNameMaxLength);

        builder.Property(x => x.DefaultCurrencyCode)
            .IsRequired()
            .HasMaxLength(ValidationConstants.DefaultCurrencyCodeLength);

        // Natural key indices for fast lookup
        builder.HasIndex(x => x.Iso2Code)
            .IsUnique()
            .HasDatabaseName("IX_Country_Iso2Code");

        builder.HasIndex(x => x.Iso3Code)
            .IsUnique()
            .HasDatabaseName("IX_Country_Iso3Code");

        // Foreign key to Currency
        builder.HasOne(x => x.DefaultCurrency)
            .WithMany()
            .HasForeignKey(x => x.DefaultCurrencyId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // One-to-many: Country to Regions
        builder.HasMany(x => x.Regions)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}