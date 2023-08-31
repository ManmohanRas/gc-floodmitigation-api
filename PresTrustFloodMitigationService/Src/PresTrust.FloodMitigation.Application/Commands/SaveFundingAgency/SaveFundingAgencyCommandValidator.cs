namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFundingAgencyCommandValidator: AbstractValidator<SaveFundingAgencyCommand>
{
    public SaveFundingAgencyCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
   .GreaterThan(0).WithMessage("Not a valid Application Id");

        RuleFor(command => command.FundingAgencyName)
            .NotNull().NotEmpty().WithMessage("Funding Agency cannot be empty");
    }
}
