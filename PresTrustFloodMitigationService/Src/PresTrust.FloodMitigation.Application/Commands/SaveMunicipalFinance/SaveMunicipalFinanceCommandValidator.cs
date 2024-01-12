namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalFinanceCommandValidator: AbstractValidator<SaveMunicipalFinanceCommand>
{
    public SaveMunicipalFinanceCommandValidator()
    {
        RuleFor(command => command.AgencyId)
                .GreaterThan(0)
                .WithMessage("Not a valid Agency Id.");
    }
}
