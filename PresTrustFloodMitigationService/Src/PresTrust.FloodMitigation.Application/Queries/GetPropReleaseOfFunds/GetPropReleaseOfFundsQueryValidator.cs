namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropReleaseOfFundsQueryValidator : AbstractValidator<GetPropReleaseOfFundsQuery>
{
    /// <summary>
    /// Create rules for attributes
    /// </summary>
    public GetPropReleaseOfFundsQueryValidator() 
    {
        RuleFor(query => query.ApplicationId)
            .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
