namespace PresTrust.FloodMitigation.Application.Commands;

public class ActivateApplicationCommandValidator : AbstractValidator<ActivateApplicationCommand>
{
    public ActivateApplicationCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
