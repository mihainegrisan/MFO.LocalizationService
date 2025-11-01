using AutoMapper;
using MFO.LocalizationService.Application.DTOs.Country;
using MFO.LocalizationService.Application.DTOs.Currency;
using MFO.LocalizationService.Application.DTOs.ExchangeRate;
using MFO.LocalizationService.Application.DTOs.Region;
using MFO.LocalizationService.Domain.Entities;

namespace MFO.LocalizationService.Application.Mapping;

public class LocalizationServiceProfile : Profile
{
    public LocalizationServiceProfile()
    {
        #region Map Domain entities to DTOs

        CreateMap<Country, CountryDto>();
        CreateMap<Currency, CurrencyDto>();
        CreateMap<Region, RegionDto>();
        CreateMap<ExchangeRate, ExchangeRateDto>();

        #endregion

        #region Map Domain entities to light DTOs

        CreateMap<Region, RegionLightDto>();
        CreateMap<Currency, CurrencyLightDto>();

        #endregion
        

        CreateMap<CreateCountryDto, Country>();
    }
}