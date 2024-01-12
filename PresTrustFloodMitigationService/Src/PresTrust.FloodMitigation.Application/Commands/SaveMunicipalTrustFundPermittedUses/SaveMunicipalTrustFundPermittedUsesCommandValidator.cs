using FluentValidation;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalTrustFundPermittedUsesCommandValidator: AbstractValidator<SaveMunicipalTrustFundPermittedUsesCommand>
{
    public SaveMunicipalTrustFundPermittedUsesCommandValidator()
    {
        RuleFor(command => command.AgencyId)
                .GreaterThan(0)
                .WithMessage("Not a valid Agency Id.");

        RuleFor(command => command.YearOfInception)
                .NotNull().NotEmpty()
                .WithMessage("Not a valid Agency Id.");
    }
}
