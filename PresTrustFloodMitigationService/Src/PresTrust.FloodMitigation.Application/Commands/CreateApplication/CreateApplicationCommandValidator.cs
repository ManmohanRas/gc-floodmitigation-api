namespace PresTrust.FloodMitigation.Application.Commands.CreateApplication;

/// <summary>
/// create rules for attributes
/// </summary>
public class CreateApplicationCommandValidator: AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationCommandValidator() 
    { 
        RuleFor(command => command.AgencyId)
             .GreaterThan(0).WithMessage("Not a valid organization");
    }
}
