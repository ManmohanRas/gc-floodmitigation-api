namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyDetailsCommandValidator : AbstractValidator<SavePropertyDetailsCommand>
{
    public SavePropertyDetailsCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
