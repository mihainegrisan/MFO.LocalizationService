namespace MFO.LocalizationService.Application.DTOs.ExchangeRate;

public sealed record UpdateExchangeRateDto
{
    public required Guid Id { get; init; }
    public string? BaseCurrencyCode { get; init; }
    public string? TargetCurrencyCode { get; init; }
    public decimal? Rate { get; init; }
    public DateTime EffectiveDate { get; init; }

    public Guid? BaseCurrencyId { get; init; }
    public Guid? TargetCurrencyId { get; init; }
}