namespace PresTrust.FloodMitigation.Application.Commands;

public class RejectPropertyCommandValidator : AbstractValidator<RejectPropertyCommand>
{
    public RejectPropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
