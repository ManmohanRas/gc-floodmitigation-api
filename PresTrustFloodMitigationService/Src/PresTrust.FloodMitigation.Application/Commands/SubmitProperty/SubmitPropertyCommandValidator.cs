namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitPropertyCommandValidator : AbstractValidator<SubmitPropertyCommand>
{
    public SubmitPropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
