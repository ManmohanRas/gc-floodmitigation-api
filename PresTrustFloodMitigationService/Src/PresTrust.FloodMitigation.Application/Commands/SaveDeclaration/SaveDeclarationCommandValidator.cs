using FluentValidation;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveDeclarationCommandValidator : AbstractValidator<SaveDeclarationCommand>
{
    public SaveDeclarationCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
             .GreaterThan(0).WithMessage("Not a valid application");
        RuleFor(command => command.Title)
             .NotEmpty().WithMessage("Title must not be empty");
        RuleFor(command => command.AgencyId)
             .GreaterThan(0).WithMessage("Not a valid agency");
        RuleFor(command => command.ApplicationType)
             .NotEmpty().WithMessage("Not a valid application type");
        RuleFor(command => command.ApplicationSubType)
             .NotEmpty().WithMessage("Not a valid application sub type");
        RuleFor(command => command.PamsPins)
             .Must(o => o.Count > 0).WithMessage("Atleast one property must be added");
        RuleFor(command => command.ApplicationUsers)
             .Must(o => o.Count > 0).WithMessage("Atleast one application user must be added");
    }
}
