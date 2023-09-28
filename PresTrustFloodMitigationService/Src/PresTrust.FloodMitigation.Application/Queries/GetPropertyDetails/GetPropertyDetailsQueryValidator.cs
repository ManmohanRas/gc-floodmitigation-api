namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyDetailsQueryValidator : AbstractValidator<GetPropertyDetailsQuery>
{
    public GetPropertyDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
               .GreaterThan(0).WithMessage("Not a valid Application Id");
        RuleFor(query => query.PamsPin)
               .NotEmpty().WithMessage("Not a valid PamsPin");
    }
}
