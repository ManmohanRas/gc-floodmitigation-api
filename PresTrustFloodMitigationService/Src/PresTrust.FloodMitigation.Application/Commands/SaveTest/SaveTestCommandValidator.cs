namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveTestCommandValidator : AbstractValidator<SaveTestCommand>
{
    public SaveTestCommandValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Not a valid Id");

        RuleFor(command => command.Name)
            .NotNull().NotEmpty().WithMessage("Not a Name");
    }
}
