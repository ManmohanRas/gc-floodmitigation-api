namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSignatoryCommandValidator : AbstractValidator<SaveSignatoryCommand>
{
    public SaveSignatoryCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application ID.");

    }
}
