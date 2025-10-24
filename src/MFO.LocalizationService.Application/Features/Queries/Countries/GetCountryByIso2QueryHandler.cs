using AutoMapper;
using FluentResults;
using MediatR;
using MFO.LocalizationService.Application.DTOs;
using MFO.LocalizationService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace MFO.LocalizationService.Application.Features.Queries.Countries;

public sealed record GetCountryByIso2Query(string Iso2Code) : IRequest<Result<CountryDto>>;

public class GetCountryByIso2QueryHandler : IRequestHandler<GetCountryByIso2Query, Result<CountryDto>>
{
    private readonly LocalizationContext _db;
    private readonly HybridCache _cache;
    private readonly IMapper _mapper;

    public GetCountryByIso2QueryHandler(
        LocalizationContext db,
        HybridCache cache,
        IMapper mapper)
    {
        _db = db;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<Result<CountryDto>> Handle(GetCountryByIso2Query request, CancellationToken cancellationToken)
    {
        var country = await _cache.GetOrCreateAsync(
            $"country:iso2:{request.Iso2Code}", // Unique key to the cache entry
            async token =>
            {
                return await _db.Countries
                    .Include(c => c.Regions)
                    .FirstOrDefaultAsync(c => c.Iso2Code == request.Iso2Code, token);
            },
            cancellationToken: cancellationToken);

        if (country is null)
        {
            return Result.Fail($"Country with ISO2 code {request.Iso2Code} not found.");
        }

        return Result.Ok(_mapper.Map<CountryDto>(country));
    }
}