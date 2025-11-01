namespace MFO.LocalizationService.Application.DTOs.Currency;

public sealed record CurrencyLightDto
{
    public required Guid CurrencyId { get; init; }
    public required string IsoCode { get; init; }
    public required string Name { get; init; }
    public required string Symbol { get; init; }
    public required int DecimalPlaces { get; init; }
}
