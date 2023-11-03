namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationAdminDetailsCommandValidater : AbstractValidator<SaveApplicationAdminDetailsCommand>
{
    public SaveApplicationAdminDetailsCommandValidater()
    {
        RuleFor(command => command.ApplicationId)
               .GreaterThan(0)
               .WithMessage("Not a valid Application Id.");
    }

}
