namespace PresTrust.FloodMitigation.Application.Commands;

public class ApprovePropertyCommandValidator : AbstractValidator<ApprovePropertyCommand>
{
    public ApprovePropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
