namespace MFO.LocalizationService.Application.DTOs.Country;

public sealed record UpdateCountryDto
{
    public required Guid CountryId { get; init; }
    public string? Iso2Code { get; init; }
    public string? Iso3Code { get; init; }
    public string? Name { get; init; }
    public string? DefaultCurrencyCode { get; init; }

    public Guid? DefaultCurrencyId { get; init; }
    public ICollection<Guid>? RegionIds { get; init; }
}