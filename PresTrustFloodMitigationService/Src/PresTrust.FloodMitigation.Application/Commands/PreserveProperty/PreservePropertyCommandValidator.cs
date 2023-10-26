namespace PresTrust.FloodMitigation.Application.Commands;

public class PreservePropertyCommandValidator : AbstractValidator<PreservePropertyCommand>
{
    public PreservePropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
