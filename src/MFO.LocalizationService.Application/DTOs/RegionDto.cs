namespace MFO.LocalizationService.Application.DTOs;

public class RegionDto
{
    public required Guid RegionId { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }

    public CountryDto? CountryDto { get; set; }
}