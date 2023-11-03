namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelTrackingCommandValidator  : AbstractValidator<SaveParcelTrackingCommand>
{
    public SaveParcelTrackingCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
