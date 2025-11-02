using FluentResults;
using MediatR;
using MFO.LocalizationService.Application.DTOs.Country;
using MFO.LocalizationService.Application.Interfaces.Repositories;
using MFO.LocalizationService.Domain.Errors;
using Microsoft.Extensions.Caching.Hybrid;

namespace MFO.LocalizationService.Application.Features.Queries.Countries;

public sealed record GetCountryByIdQuery(Guid CountryId) : IRequest<Result<CountryDto>>;

public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Result<CountryDto>>
{
    private readonly HybridCache _cache;
    private readonly IReadOnlyCountryRepository _readOnlyReadOnlyCountryRepository;

    public GetCountryByIdQueryHandler(
        HybridCache cache,
        IReadOnlyCountryRepository readOnlyCountryRepository)
    {
        _cache = cache;
        _readOnlyReadOnlyCountryRepository = readOnlyCountryRepository;
    }

    public async Task<Result<CountryDto>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var countryDto = await _cache.GetOrCreateAsync(
            $"country:id:{request.CountryId}",
            async token => await _readOnlyReadOnlyCountryRepository.GetCountryById(request.CountryId, token),
            cancellationToken: cancellationToken);

        if (countryDto is null)
        {
            return Result.Fail(new NotFoundError($"Country with Id '{request.CountryId}' not found."));
        }

        return Result.Ok(countryDto);
    }
}