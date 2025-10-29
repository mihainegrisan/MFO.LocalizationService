using MFO.LocalizationService.Domain.Common;
using MFO.LocalizationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFO.LocalizationService.Infrastructure.Persistence.Configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasKey(x => x.RegionId);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(ValidationConstants.RegionCodeLength);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(ValidationConstants.RegionNameMaxLength);

        // Composite natural key index (country + code)
        builder.HasIndex(x => new { x.CountryId, x.Code })
            .IsUnique()
            .HasDatabaseName("IX_Region_Country_Code");

        // Foreign key
        builder.HasOne(x => x.Country)
            .WithMany(x => x.Regions)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}