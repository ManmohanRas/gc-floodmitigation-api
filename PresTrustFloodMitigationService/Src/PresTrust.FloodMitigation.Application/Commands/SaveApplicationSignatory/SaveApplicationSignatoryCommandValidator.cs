namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationSignatoryCommandValidator : AbstractValidator<SaveApplicationSignatoryCommand>
{
    public SaveApplicationSignatoryCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");

    }
}
