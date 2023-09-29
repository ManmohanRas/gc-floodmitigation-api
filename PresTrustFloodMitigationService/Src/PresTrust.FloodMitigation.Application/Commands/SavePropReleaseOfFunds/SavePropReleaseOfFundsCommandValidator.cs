namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropReleaseOfFundsCommandValidator : AbstractValidator<SavePropReleaseOfFundsCommand>
{
    public SavePropReleaseOfFundsCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
