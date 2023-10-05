namespace PresTrust.FloodMitigation.Application.Commands;

public class CloseApplicationCommandValidator : AbstractValidator<CloseApplicationCommand>
{
    public CloseApplicationCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
