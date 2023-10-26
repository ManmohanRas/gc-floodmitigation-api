namespace PresTrust.FloodMitigation.Application.Commands;

public class WithdrawPropertyCommandValidator : AbstractValidator<WithdrawPropertyCommand>
{
    public WithdrawPropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
