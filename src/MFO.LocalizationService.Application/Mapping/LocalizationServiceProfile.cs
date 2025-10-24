using AutoMapper;
using MFO.LocalizationService.Application.DTOs;
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