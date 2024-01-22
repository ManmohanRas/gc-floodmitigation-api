namespace PresTrust.FloodMitigation.Application.Queries;

public class GetMunicipalCommentsQueryValidator: AbstractValidator<GetMunicipalCommentsQuery>
{
    public GetMunicipalCommentsQueryValidator()
    {
        RuleFor(query => query.AgencyId)
                .Cascade(CascadeMode.Stop)
                .NotNull().NotEmpty().WithMessage("AgencyId is required.")
                .GreaterThan(0)
                .WithMessage("AgencyId must be greater than 0");
    }
}
