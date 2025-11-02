using AutoMapper;
using AutoMapper.QueryableExtensions;
using MFO.LocalizationService.Application.DTOs.Country;
using MFO.LocalizationService.Application.Interfaces.Repositories;
using MFO.LocalizationService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MFO.LocalizationService.Infrastructure.Repositories;

public class CountryReadOnlyRepository : IReadOnlyCountryRepository
{
    private readonly LocalizationDbContext _db;
    private readonly IMapper _mapper;

    public CountryReadOnlyRepository(LocalizationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<CountryDto?> GetCountryById(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Countries
            .AsNoTracking()
            .Where(c => c.CountryId == id)
            .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CountryDto?> GetCountryByIso2Code(string iso2Code, CancellationToken cancellationToken)
    {
        return await _db.Countries
            .AsNoTracking()
            .Where(c => c.Iso2Code == iso2Code)
            .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}