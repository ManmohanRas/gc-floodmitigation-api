namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitDeclarationCommandValidator : AbstractValidator<SubmitDeclarationCommand>
{
    public SubmitDeclarationCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
