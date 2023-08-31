namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteFundingAgencyCommandValidator: AbstractValidator<DeleteFundingAgencyCommand>
{
    public DeleteFundingAgencyCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0)
            .WithMessage("Not a valid Application Id");

        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid Feedback Id");
    }
}
