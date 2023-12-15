namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDocumentCommandValidator: AbstractValidator<SaveFlapDocumentCommand>
{
    public SaveFlapDocumentCommandValidator()
    {
        RuleFor(command => command.AgencyId)
                .GreaterThan(0)
                .WithMessage("Not a valid Agency Id.");
    }
}
