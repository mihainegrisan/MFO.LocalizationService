namespace MFO.LocalizationService.Domain.Common;

public static class ValidationConstants
{
    public const int CountryNameMaxLength = 50;
    public const int DescriptionMaxLength = 500;

    public const int Iso2CodeLength = 2;
    public const int Iso3CodeLength = 3;

    public const int DefaultCurrencyCodeLength = Iso3CodeLength;
}