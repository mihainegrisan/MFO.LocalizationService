using MFO.LocalizationService.Application.DTOs.ExchangeRate;

namespace MFO.LocalizationService.Application.DTOs.Currency;

public sealed record CurrencyDto
{
    public required Guid CurrencyId { get; init; }
    public required string IsoCode { get; init; }
    public required string Name { get; init; }
    public required string Symbol { get; init; }
    public required int DecimalPlaces { get; init; }

    public required ICollection<ExchangeRateDto> BaseExchangeRates { get; init; }
    public required ICollection<ExchangeRateDto> TargetExchangeRates { get; init; }
}