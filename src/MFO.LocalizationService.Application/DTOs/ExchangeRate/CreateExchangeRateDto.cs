namespace MFO.LocalizationService.Application.DTOs.ExchangeRate;

public sealed record CreateExchangeRateDto
{
    public required string BaseCurrencyCode { get; init; }
    public required string TargetCurrencyCode { get; init; }
    public required decimal Rate { get; init; }
    public required DateTime EffectiveDate { get; init; }

    public required Guid BaseCurrencyId { get; init; }
    public required Guid TargetCurrencyId { get; init; }
}