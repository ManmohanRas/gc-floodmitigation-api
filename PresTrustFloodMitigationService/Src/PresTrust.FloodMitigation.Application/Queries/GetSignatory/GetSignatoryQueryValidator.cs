namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSignatoryQueryValidator : AbstractValidator<GetSignatoryQuery>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public GetSignatoryQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
