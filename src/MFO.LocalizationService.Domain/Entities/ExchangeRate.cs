using MFO.LocalizationService.Domain.Common;

namespace MFO.LocalizationService.Domain.Entities;

public class ExchangeRate : AuditableEntity
{
    public required Guid Id { get; set; }
    public required string BaseCurrencyCode { get; set; }
    public required string TargetCurrencyCode { get; set; }
    public required decimal Rate { get; set; }
    public required DateTime EffectiveDate { get; set; }

    public Guid? BaseCurrencyId { get; set; }
    public Currency? BaseCurrency { get; set; }
    public Guid? TargetCurrencyId { get; set; }
    public Currency? TargetCurrency { get; set; }
}