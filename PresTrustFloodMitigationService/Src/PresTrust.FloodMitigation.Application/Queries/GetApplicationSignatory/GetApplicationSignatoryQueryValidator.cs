namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationSignatoryQueryValidator : AbstractValidator<GetApplicationSignatoryQuery>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public GetApplicationSignatoryQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
