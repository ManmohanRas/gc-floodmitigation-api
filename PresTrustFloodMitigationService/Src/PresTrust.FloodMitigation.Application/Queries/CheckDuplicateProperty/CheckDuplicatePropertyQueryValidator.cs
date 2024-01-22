namespace PresTrust.FloodMitigation.Application.Queries;

public class CheckDuplicatePropertyQueryValidator : AbstractValidator<CheckDuplicatePropertyQuery>
{
    public CheckDuplicatePropertyQueryValidator()
    {
        RuleFor(query => query.PamsPin)
           .NotEmpty().WithMessage("Not a valid PamsPin");
    }
}
