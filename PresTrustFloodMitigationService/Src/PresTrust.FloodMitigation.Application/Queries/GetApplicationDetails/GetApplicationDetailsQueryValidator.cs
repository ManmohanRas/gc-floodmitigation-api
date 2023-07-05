namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationDetailsQueryValidator : AbstractValidator<GetApplicationDetailsQuery>
{
    public GetApplicationDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
               .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
