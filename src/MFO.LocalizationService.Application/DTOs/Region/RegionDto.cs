using MFO.LocalizationService.Application.DTOs.Country;

namespace MFO.LocalizationService.Application.DTOs.Region;

public sealed record RegionDto
{
    public required Guid RegionId { get; init; }
    public required string Code { get; init; }
    public required string Name { get; init; }

    public CountryDto? CountryDto { get; init; }
}