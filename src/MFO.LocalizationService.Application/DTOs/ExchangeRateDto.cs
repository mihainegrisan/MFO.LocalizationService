namespace MFO.LocalizationService.Application.DTOs;

public class ExchangeRateDto
{
    public required Guid Id { get; set; }
    public required string BaseCurrencyCode { get; set; }
    public required string TargetCurrencyCode { get; set; }
    public required decimal Rate { get; set; }
    public required DateTime EffectiveDate { get; set; }

    public CurrencyDto? BaseCurrency { get; set; }
    public CurrencyDto? TargetCurrency { get; set; }
}