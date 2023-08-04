namespace PresTrust.FloodMitigation.Application.Commands;

public class RejectApplicationCommandValidator : AbstractValidator<RejectApplicationCommand>
{
    public RejectApplicationCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
