namespace MFO.LocalizationService.Application.DTOs.Currency;

public sealed record CreateCurrencyDto
{
    public required string IsoCode { get; init; }
    public required string Name { get; init; }
    public required string Symbol { get; init; }
    public required int DecimalPlaces { get; init; }

    public required ICollection<Guid> BaseExchangeRateIds { get; init; }
    public required ICollection<Guid> TargetExchangeRateIds { get; init; }
}