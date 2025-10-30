namespace MFO.LocalizationService.Application.DTOs.Region;

public sealed record UpdateRegionDto
{
    public required Guid RegionId { get; init; }
    public string? Code { get; init; }
    public string? Name { get; init; }

    public Guid? CountryId { get; init; }
}