namespace PresTrust.FloodMitigation.Application.Commands;

public class ReviewPropertyCommandValidator : AbstractValidator<ReviewPropertyCommand>
{
    public ReviewPropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
