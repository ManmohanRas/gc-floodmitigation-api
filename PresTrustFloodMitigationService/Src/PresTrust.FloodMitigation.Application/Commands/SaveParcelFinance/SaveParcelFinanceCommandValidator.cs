namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelFinanceCommandValidator: AbstractValidator<SaveParcelFinanceCommand>
{
    public SaveParcelFinanceCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
