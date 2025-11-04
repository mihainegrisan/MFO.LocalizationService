using FluentResults;
using MediatR;
using MFO.LocalizationService.Application.DTOs.Country;
using MFO.LocalizationService.Application.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Hybrid;

namespace MFO.LocalizationService.Application.Features.Queries.Countries;

public sealed record GetCountriesQuery : IRequest<Result<IReadOnlyList<CountryDto>>>;

public class GetCountriesHandler : IRequestHandler<GetCountriesQuery, Result<IReadOnlyList<CountryDto>>>
{
    private readonly HybridCache _cache;
    private readonly IReadOnlyCountryRepository _readOnlyCountryRepository;

    public GetCountriesHandler(
        HybridCache cache,
        IReadOnlyCountryRepository readOnlyCountryRepository)
    {
        _cache = cache;
        _readOnlyCountryRepository = readOnlyCountryRepository;
    }

    public async Task<Result<IReadOnlyList<CountryDto>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        var countriesDto = await _cache.GetOrCreateAsync(
            $"countries:all",
            async token => await _readOnlyCountryRepository.GetCountries(token),
            cancellationToken: cancellationToken);

        if (countriesDto.Count is 0)
        {
            return Result.Ok<IReadOnlyList<CountryDto>>(new List<CountryDto>());
        }

        return Result.Ok(countriesDto);
    }
}