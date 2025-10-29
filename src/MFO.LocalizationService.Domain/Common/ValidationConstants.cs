namespace MFO.LocalizationService.Domain.Common;

public static class ValidationConstants
{
    public const int Iso2CodeLength = 2;
    public const int Iso3CodeLength = 3;

    #region Country

    public const int CountryNameMaxLength = 50;
    public const int DefaultCurrencyCodeLength = Iso3CodeLength;

    #endregion

    #region Region

    public const int RegionNameMaxLength = 50;
    public const int RegionCodeLength = Iso2CodeLength;

    #endregion

    #region Currency

    public const int CurrencyCodeLength = Iso3CodeLength;
    public const int CurrencyNameMaxLength = 50;
    public const int CurrencySymbolLength = 1;
    public const int CurrencyDecimalPlacesDefaultValue = 2;

    #endregion

}