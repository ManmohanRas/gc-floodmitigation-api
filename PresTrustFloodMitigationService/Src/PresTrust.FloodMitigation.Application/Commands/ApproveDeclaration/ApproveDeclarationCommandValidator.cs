namespace PresTrust.FloodMitigation.Application.Commands;

public class ApproveDeclarationCommandValidator : AbstractValidator<ApproveDeclarationCommand>
{
    public ApproveDeclarationCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
