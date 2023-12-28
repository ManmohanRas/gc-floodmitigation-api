namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapTargetAreaCommandValidator: AbstractValidator<SaveFlapTargetAreaCommand>
{
    public SaveFlapTargetAreaCommandValidator()
    {
        RuleFor(command => command.AgencyId)
                .GreaterThan(0)
                .WithMessage("Not a valid Agency Id.");
    }
}
