namespace MFO.LocalizationService.Application.DTOs.Region;

public sealed record CreateRegionDto
{
    public required string Code { get; init; }
    public required string Name { get; init; }

    public required Guid CountryId { get; init; }
}