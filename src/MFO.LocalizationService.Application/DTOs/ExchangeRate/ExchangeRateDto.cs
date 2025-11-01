using MFO.LocalizationService.Application.DTOs.Currency;

namespace MFO.LocalizationService.Application.DTOs.ExchangeRate;

public sealed record ExchangeRateDto
{
    public required Guid Id { get; init; }
    public required string BaseCurrencyCode { get; init; }
    public required string TargetCurrencyCode { get; init; }
    public required decimal Rate { get; init; }
    public required DateTime EffectiveDate { get; init; }

    public required CurrencyLightDto BaseCurrency { get; init; }
    public required CurrencyLightDto TargetCurrency { get; init; }
}