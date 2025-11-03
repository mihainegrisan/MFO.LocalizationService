using MediatR;
using MFO.LocalizationService.Application.DTOs.Country;
using MFO.LocalizationService.Application.Features.Queries.Countries;
using MFO.LocalizationService.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace MFO.LocalizationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CountriesController> _logger;

    public CountriesController(IMediator mediator, ILogger<CountriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{iso2Code}")]
    [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIso2Code(string iso2Code, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for country with Id: {CountryIso2Code}", iso2Code);

        var query = new GetCountryByIso2Query(iso2Code);
        
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsFailed)
        {
            if (result.HasError<NotFoundError>())
            {
                _logger.LogInformation("Country with Iso2Code: {CountryIso2Code} not found.", iso2Code);

                return NotFound();
            }

            _logger.LogWarning("Failed to retrieve country with Iso2Code: {CountryIso2Code}. Errors: {@Errors}", iso2Code, result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Country with Iso2Code: {CountryIso2Code} retrieved successfully.", iso2Code);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for country with Id: {CountryId}", id);

        var result = await _mediator.Send(new GetCountryByIdQuery(id), cancellationToken);

        if (result.IsFailed)
        {
            if (result.HasError<NotFoundError>())
            {
                _logger.LogInformation("Country with Id: {CountryId} not found.", id);

                return NotFound();
            }

            _logger.LogWarning("Failed to retrieve country with Id: {CountryId}. Errors: {@Errors}", id, result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Country with Id: {CountryId} retrieved successfully.", id);

        return Ok(result.Value);
    }
}