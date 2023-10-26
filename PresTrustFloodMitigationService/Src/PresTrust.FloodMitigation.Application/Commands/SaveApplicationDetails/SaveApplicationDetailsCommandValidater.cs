namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationDetailsCommandValidater : AbstractValidator<SaveApplicationDetailsCommand>
{
    public SaveApplicationDetailsCommandValidater()
    {
        RuleFor(command => command.ApplicationId)
               .GreaterThan(0)
               .WithMessage("Not a valid Application Id.");
    }

}
