namespace MFO.LocalizationService.Application.DTOs.Currency;

public sealed record UpdateCurrencyDto
{
    public required Guid CurrencyId { get; init; }
    public string? IsoCode { get; init; }
    public string? Name { get; init; }
    public string? Symbol { get; init; }
    public int? DecimalPlaces { get; init; }

    public ICollection<Guid>? BaseExchangeRateIds { get; init; }
    public ICollection<Guid>? TargetExchangeRateIds { get; init; }
}