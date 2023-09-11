namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveContactCommandValidator : AbstractValidator<SaveContactCommand>
{
    public SaveContactCommandValidator()
    {
        RuleFor(query => query.ContactName)
              .NotNull().NotEmpty()
              .WithMessage("ContactName cannot be empty.");

        RuleFor(query => query.ApplicationId)
            .GreaterThan(0)
            .WithMessage("ApplicationId must be greater than 0");
    }
}
