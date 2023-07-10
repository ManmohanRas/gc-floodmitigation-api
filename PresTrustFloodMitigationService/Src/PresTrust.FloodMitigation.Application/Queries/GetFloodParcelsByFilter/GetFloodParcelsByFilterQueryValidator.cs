namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFloodParcelsByFilterQueryValidator : AbstractValidator<GetFloodParcelsByFilterQuery>
{
    public GetFloodParcelsByFilterQueryValidator()
    {
        RuleFor(query => query.AgencyId)
               .GreaterThan(0).WithMessage("Not a valid Agency");
    }
}
