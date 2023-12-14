namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDetailsCommandValidator: AbstractValidator<SaveFlapDetailsCommand>
{
    public SaveFlapDetailsCommandValidator() 
    {
        RuleFor(command => command.AgencyId)
                .GreaterThan(0)
                .WithMessage("Not a valid Agency Id.");
    }
}
