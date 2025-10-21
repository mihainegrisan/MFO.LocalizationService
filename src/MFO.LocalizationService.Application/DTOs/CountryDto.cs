namespace MFO.LocalizationService.Application.DTOs;

public class CountryDto
{
    public required Guid CountryId { get; set; }
    public required string Iso2Code { get; set; }
    public required string Iso3Code { get; set; }
    public required string Name { get; set; }
    public required string DefaultCurrencyCode { get; set; }

    public CurrencyDto? DefaultCurrencyDto { get; set; }
    public ICollection<RegionDto>? Regions { get; set; }
}