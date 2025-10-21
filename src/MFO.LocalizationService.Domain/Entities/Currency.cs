namespace MFO.LocalizationService.Domain.Entities;

public class Currency
{
    public required Guid CurrencyId { get; set; }
    public required string IsoCode { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }
    public required int DecimalPlaces { get; set; }
    
    //public bool IsActive { get; set; }

    public ICollection<ExchangeRate>? BaseExchangeRates { get; set; }
    public ICollection<ExchangeRate>? TargetExchangeRates { get; set; }
}