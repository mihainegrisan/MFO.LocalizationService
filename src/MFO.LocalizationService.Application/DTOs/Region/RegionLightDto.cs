namespace MFO.LocalizationService.Application.DTOs.Region;

public class RegionLightDto
{
    public required Guid RegionId { get; init; }
    public required string Code { get; init; }
    public required string Name { get; init; }
}