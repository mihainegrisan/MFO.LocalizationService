using MFO.LocalizationService.Application.DTOs.Currency;
using MFO.LocalizationService.Application.DTOs.Region;

namespace MFO.LocalizationService.Application.DTOs.Country;

public sealed record CountryDto
{
    public required Guid CountryId { get; init; }
    public required string Iso2Code { get; init; }
    public required string Iso3Code { get; init; }
    public required string Name { get; init; }
    public required string DefaultCurrencyCode { get; init; }

    public required CurrencyDto DefaultCurrency { get; init; }
    public required ICollection<RegionLightDto> Regions { get; init; }
}