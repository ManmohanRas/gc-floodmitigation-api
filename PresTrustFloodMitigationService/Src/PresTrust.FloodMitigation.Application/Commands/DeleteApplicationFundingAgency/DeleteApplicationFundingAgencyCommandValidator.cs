namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationFundingAgencyCommandValidator: AbstractValidator<DeleteApplicationFundingAgencyCommand>
{
    public DeleteApplicationFundingAgencyCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0)
            .WithMessage("Not a valid Application Id");

        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid Feedback Id");
    }
}
