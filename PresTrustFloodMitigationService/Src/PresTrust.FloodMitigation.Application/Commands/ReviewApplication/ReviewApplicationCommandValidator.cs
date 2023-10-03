namespace PresTrust.FloodMitigation.Application.Commands;

public class ReviewApplicationCommandValidator : AbstractValidator<ReviewApplicationCommand>
{
    public ReviewApplicationCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
