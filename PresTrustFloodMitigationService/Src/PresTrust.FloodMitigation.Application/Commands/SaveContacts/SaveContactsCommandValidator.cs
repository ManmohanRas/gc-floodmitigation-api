namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveContactsCommandValidator : AbstractValidator<SaveContactsCommand>
{
    public SaveContactsCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
            .GreaterThan(0)
            .WithMessage("ApplicationId must be greater than 0");
    }
}
