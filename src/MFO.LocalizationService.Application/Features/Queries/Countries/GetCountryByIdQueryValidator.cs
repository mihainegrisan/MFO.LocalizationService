using FluentValidation;

namespace MFO.LocalizationService.Application.Features.Queries.Countries;

public class GetCountryByIdQueryValidator : AbstractValidator<GetCountryByIdQuery>
{
    public GetCountryByIdQueryValidator()
    {
        RuleFor(q => q.CountryId)
            .NotNull();
    }
}