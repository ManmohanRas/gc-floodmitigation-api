namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationReleaseOfFundsCommandValidator: AbstractValidator<SaveApplicationReleaseOfFundsCommand>
{
    public SaveApplicationReleaseOfFundsCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
               .GreaterThan(0)
               .WithMessage("Not a valid Application Id.");
    }
}
