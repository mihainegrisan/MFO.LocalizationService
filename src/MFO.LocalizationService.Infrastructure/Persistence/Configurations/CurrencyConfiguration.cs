using MFO.LocalizationService.Domain.Common;
using MFO.LocalizationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFO.LocalizationService.Infrastructure.Persistence.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(x => x.CurrencyId);

        builder.Property(x => x.IsoCode)
            .IsRequired()
            .HasMaxLength(ValidationConstants.CurrencyCodeLength);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(ValidationConstants.CurrencyNameMaxLength);

        builder.Property(x => x.Symbol)
            .IsRequired()
            .HasMaxLength(ValidationConstants.CurrencySymbolLength);

        builder.Property(x => x.DecimalPlaces)
            .IsRequired()
            .HasDefaultValue(ValidationConstants.CurrencyDecimalPlacesDefaultValue);

        builder.HasIndex(x => x.IsoCode)
            .IsUnique()
            .HasDatabaseName("IX_Currency_IsoCode");

        builder.HasMany(x => x.BaseExchangeRates)
            .WithOne(x => x.BaseCurrency)
            .HasForeignKey(x => x.BaseCurrencyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.TargetExchangeRates)
            .WithOne(x => x.TargetCurrency)
            .HasForeignKey(x => x.TargetCurrencyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}