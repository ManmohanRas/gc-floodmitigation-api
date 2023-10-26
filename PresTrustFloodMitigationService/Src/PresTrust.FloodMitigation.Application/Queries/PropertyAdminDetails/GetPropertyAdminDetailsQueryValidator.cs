namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyAdminDetailsQueryValidator : AbstractValidator<GetPropertyAdminDetailsQuery>
{
    /// <summary>
    /// Create rules for attributes
    /// </summary>
    public GetPropertyAdminDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
            .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
