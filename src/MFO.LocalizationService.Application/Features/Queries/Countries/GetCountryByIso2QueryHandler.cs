using FluentResults;
using MediatR;
using MFO.LocalizationService.Application.DTOs.Country;
using MFO.LocalizationService.Application.Interfaces.Repositories;
using MFO.LocalizationService.Domain.Errors;
using Microsoft.Extensions.Caching.Hybrid;

namespace MFO.LocalizationService.Application.Features.Queries.Countries;

public sealed record GetCountryByIso2Query(string Iso2Code) : IRequest<Result<CountryDto>>;

public class GetCountryByIso2QueryHandler : IRequestHandler<GetCountryByIso2Query, Result<CountryDto>>
{
    private readonly HybridCache _cache;
    private readonly IReadOnlyCountryRepository _readOnlyCountryRepository;

    public GetCountryByIso2QueryHandler(
        HybridCache cache,
        IReadOnlyCountryRepository readOnlyCountryRepository)
    {
        _cache = cache;
        _readOnlyCountryRepository = readOnlyCountryRepository;
    }

    public async Task<Result<CountryDto>> Handle(GetCountryByIso2Query request, CancellationToken cancellationToken)
    {
        var countryDto = await _cache.GetOrCreateAsync(
            $"country:iso2:{request.Iso2Code}",
            async token => await _readOnlyCountryRepository.GetCountryByIso2Code(request.Iso2Code, token),
            cancellationToken: cancellationToken);

        if (countryDto is null)
        {
            return Result.Fail(new NotFoundError($"Country with ISO2 code '{request.Iso2Code}' not found."));
        }

        return Result.Ok(countryDto);
    }
}