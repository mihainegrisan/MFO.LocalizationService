namespace MFO.LocalizationService.Domain.Entities;

public class Region
{
    public required Guid RegionId { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }

    // public bool IsActive { get; set; }

    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
}