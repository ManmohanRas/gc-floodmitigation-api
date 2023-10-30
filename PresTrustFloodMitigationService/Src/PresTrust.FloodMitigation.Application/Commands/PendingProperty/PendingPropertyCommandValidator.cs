namespace PresTrust.FloodMitigation.Application.Commands;

public class PendingPropertyCommandValidator : AbstractValidator<PendingPropertyCommand>
{
    public PendingPropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
