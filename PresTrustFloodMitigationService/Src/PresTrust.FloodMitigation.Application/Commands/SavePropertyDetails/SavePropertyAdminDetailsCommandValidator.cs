namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyAdminDetailsCommandValidator : AbstractValidator<SavePropertyAdminDetailsCommand>
{
    public SavePropertyAdminDetailsCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
