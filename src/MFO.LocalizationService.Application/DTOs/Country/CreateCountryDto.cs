namespace MFO.LocalizationService.Application.DTOs.Country;

public sealed record CreateCountryDto
{
    public required string Iso2Code { get; init; }
    public required string Iso3Code { get; init; }
    public required string Name { get; init; }
    public required string DefaultCurrencyCode { get; init; }

    public required Guid DefaultCurrencyId { get; init; }
    public required ICollection<Guid> RegionIds { get; init; }
}