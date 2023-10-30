namespace PresTrust.FloodMitigation.Application.Commands;

public class TransferPropertyCommandValidator : AbstractValidator<TransferPropertyCommand>
{
    public TransferPropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
