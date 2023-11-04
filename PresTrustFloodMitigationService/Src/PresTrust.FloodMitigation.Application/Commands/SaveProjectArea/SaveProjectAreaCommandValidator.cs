namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// create rules for attributes
/// </summary>
public class SaveProjectAreaCommandValidator : AbstractValidator<SaveProjectAreaCommand>
{
    public SaveProjectAreaCommandValidator()
    {
        RuleFor(command => command.Id)
             .GreaterThan(0).WithMessage("Not a valid application");

        RuleFor(command => command.AgencyId)
             .GreaterThan(0).WithMessage("Not a valid organization");

        RuleFor(command => command.Parcels.Count)
             .GreaterThan(0).WithMessage("Atleast one property is required.");
    }
}
