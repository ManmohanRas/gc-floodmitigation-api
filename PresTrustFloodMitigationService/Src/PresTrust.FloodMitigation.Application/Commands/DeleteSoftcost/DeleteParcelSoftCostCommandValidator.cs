namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteParcelSoftCostCommandValidator: AbstractValidator<DeleteParcelSoftCostCommand>
{
    public DeleteParcelSoftCostCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0)
            .WithMessage("Not a valid Application Id");

        RuleFor(command => command.Pamspin)
           .NotEmpty()
           .WithMessage("Not a valid Pamspin");
    }
}
