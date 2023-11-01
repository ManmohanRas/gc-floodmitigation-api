namespace PresTrust.FloodMitigation.Application.Commands;

public class GrantExpirePropertyCommandValidator : AbstractValidator<GrantExpirePropertyCommand>
{
    public GrantExpirePropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
