using MFO.LocalizationService.Domain.Common;
using MFO.LocalizationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFO.LocalizationService.Infrastructure.Persistence.Configurations;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BaseCurrencyCode)
            .IsRequired()
            .HasMaxLength(ValidationConstants.CurrencyCodeLength);

        builder.Property(x => x.TargetCurrencyCode)
            .IsRequired()
            .HasMaxLength(ValidationConstants.CurrencyCodeLength);

        builder.Property(x => x.Rate)
            .HasPrecision(10, 6)
            .IsRequired();

        builder.Property(x => x.EffectiveDate)
            .IsRequired();

        // Composite index for fast rate lookups
        builder.HasIndex(x => new { x.BaseCurrencyCode, x.TargetCurrencyCode, x.EffectiveDate })
            .HasDatabaseName("IX_ExchangeRate_BaseCurrency_TargetCurrency_Date");

        // Foreign keys
        builder.HasOne(x => x.BaseCurrency)
            .WithMany(x => x.BaseExchangeRates)
            .HasForeignKey(x => x.BaseCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TargetCurrency)
            .WithMany(x => x.TargetExchangeRates)
            .HasForeignKey(x => x.TargetCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}