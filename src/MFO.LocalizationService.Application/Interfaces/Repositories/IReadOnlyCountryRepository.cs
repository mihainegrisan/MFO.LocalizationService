using MFO.LocalizationService.Application.DTOs.Country;

namespace MFO.LocalizationService.Application.Interfaces.Repositories;

public interface IReadOnlyCountryRepository
{
    Task<CountryDto?> GetCountryByIso2Code(string iso2Code, CancellationToken cancellationToken);
    Task<CountryDto?> GetCountryById(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CountryDto>> GetCountries(CancellationToken cancellationToken);
}
