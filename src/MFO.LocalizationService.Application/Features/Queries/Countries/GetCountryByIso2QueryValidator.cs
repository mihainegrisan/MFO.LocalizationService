using FluentValidation;
using MFO.LocalizationService.Domain.Common;

namespace MFO.LocalizationService.Application.Features.Queries.Countries;

public class GetCountryByIso2QueryValidator : AbstractValidator<GetCountryByIso2Query>
{
    public GetCountryByIso2QueryValidator()
    {
        RuleFor(q => q.Iso2Code)
            .NotNull()
            .NotEmpty()
            .Length(ValidationConstants.Iso2CodeLength);
    }
}