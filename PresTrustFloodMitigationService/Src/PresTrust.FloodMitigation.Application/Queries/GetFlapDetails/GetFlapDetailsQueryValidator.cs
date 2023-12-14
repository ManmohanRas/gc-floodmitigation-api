namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapDetailsQueryValidator: AbstractValidator<GetFlapDetailsQuery>
{
    public GetFlapDetailsQueryValidator()
    {
        RuleFor(query => query.AgencyId)
                .GreaterThan(0)
                .WithMessage("Not a valid Agency Id.");
    }
}
