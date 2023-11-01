namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationAdminDetailsQueryValidator : AbstractValidator<GetApplicationAdminDetailsQuery>
{
    /// <summary>
    /// Create rules for attributes
    /// </summary>
    public GetApplicationAdminDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
            .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
