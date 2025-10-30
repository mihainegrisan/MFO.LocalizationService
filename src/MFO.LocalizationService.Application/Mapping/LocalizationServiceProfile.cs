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
        CreateMap<Country, CountryDto>();
        CreateMap<Currency, CurrencyDto>();
        CreateMap<Region, RegionDto>();
        CreateMap<ExchangeRate, ExchangeRateDto>();

        CreateMap<CreateCountryDto, Country>();
    }
}