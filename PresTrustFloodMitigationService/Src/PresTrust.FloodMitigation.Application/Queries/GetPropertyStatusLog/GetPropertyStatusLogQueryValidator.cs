namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyStatusLogQueryValidator : AbstractValidator<GetPropertyStatusLogQuery>
{
    public GetPropertyStatusLogQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
               .GreaterThan(0).WithMessage("Not a valid Application Id");
        RuleFor(query => query.PamsPin)
               .NotEmpty().WithMessage("Not a valid PamsPin");
    }
}
