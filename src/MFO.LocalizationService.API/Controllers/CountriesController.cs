using MediatR;
using MFO.LocalizationService.Application.DTOs;
using MFO.LocalizationService.Application.Features.Queries.Countries;
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
        var query = new GetCountryByIso2Query(iso2Code);
        
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }
}