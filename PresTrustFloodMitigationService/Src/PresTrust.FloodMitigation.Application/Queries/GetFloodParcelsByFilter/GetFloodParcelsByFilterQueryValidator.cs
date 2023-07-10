using FluentValidation;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFloodParcelsByFilterQueryValidator : AbstractValidator<GetFloodParcelsByFilterQuery>
{
    public GetFloodParcelsByFilterQueryValidator()
    {
        RuleFor(query => query.AgencyId)
               .GreaterThan(0).WithMessage("Not a valid Agency");
        RuleFor(query => query.Block)
               .NotEmpty().WithMessage("Not a valid Block");
        RuleFor(query => query.Lot)
               .NotEmpty().WithMessage("Not a valid Lot");
        RuleFor(query => query.Address)
               .NotEmpty().WithMessage("Not a valid Address");
    }
}
