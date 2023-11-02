namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationStatusLogQueryValidator : AbstractValidator<GetApplicationStatusLogQuery>
{
    public GetApplicationStatusLogQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
               .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
