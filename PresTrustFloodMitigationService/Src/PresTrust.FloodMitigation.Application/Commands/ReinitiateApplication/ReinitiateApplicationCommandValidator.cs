namespace PresTrust.FloodMitigation.Application.Commands;

public class ReinitiateApplicationCommandValidator : AbstractValidator<ReinitiateApplicationCommand>
{
    public ReinitiateApplicationCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
