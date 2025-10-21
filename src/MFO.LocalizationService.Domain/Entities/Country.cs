namespace MFO.LocalizationService.Domain.Entities;

public class Country
{
    public required Guid CountryId { get; set; }
    public required string Iso2Code { get; set; }
    public required string Iso3Code { get; set; }
    public required string Name { get; set; }
    public required string DefaultCurrencyCode { get; set; }

    // public string? PhoneCode { get; set; }  // "+1", "+33", "+49"
    // public bool IsActive { get; set; }

    public Guid? DefaultCurrencyId { get; set; }
    public Currency? DefaultCurrency { get; set; }
    public ICollection<Region>? Regions { get; set; }
}