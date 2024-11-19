namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveWarningsCommandValidator : AbstractValidator<SaveWarningsCommand>
{
    public SaveWarningsCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
