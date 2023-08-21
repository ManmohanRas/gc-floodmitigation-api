namespace PresTrust.FloodMitigation.Application.Commands;
    public class SaveTechDetailsCommandValidator : AbstractValidator<SaveTechDetailsCommand>
{
    public SaveTechDetailsCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
