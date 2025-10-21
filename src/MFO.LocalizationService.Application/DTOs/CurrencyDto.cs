namespace MFO.LocalizationService.Application.DTOs;

public class CurrencyDto
{
    public required Guid CurrencyId { get; set; }
    public required string IsoCode { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }
    public required int DecimalPlaces { get; set; }

    public ICollection<ExchangeRateDto>? BaseExchangeRates { get; set; }
    public ICollection<ExchangeRateDto>? TargetExchangeRates { get; set; }
}