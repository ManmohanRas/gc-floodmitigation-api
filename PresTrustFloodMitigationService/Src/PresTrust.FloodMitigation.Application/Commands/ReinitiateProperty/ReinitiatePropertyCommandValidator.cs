namespace PresTrust.FloodMitigation.Application.Commands;

public class ReinitiatePropertyCommandValidator : AbstractValidator<ReinitiatePropertyCommand>
{
    public ReinitiatePropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
