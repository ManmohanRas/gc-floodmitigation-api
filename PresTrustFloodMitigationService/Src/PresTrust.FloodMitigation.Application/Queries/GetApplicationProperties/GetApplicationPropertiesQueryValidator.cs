namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationPropertiesQueryValidator : AbstractValidator<GetApplicationPropertiesQuery>
{
    public GetApplicationPropertiesQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
               .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
