namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveCommentCommandValidator : AbstractValidator<SaveCommentCommand>
{
    public SaveCommentCommandValidator() 
    {
        RuleFor(query => query.Comment)
              .NotNull().NotEmpty()
              .WithMessage("Comments cannot be empty.");

        RuleFor(query => query.ApplicationId)
            .GreaterThan(0)
            .WithMessage("ApplicationId must be greater than 0");
    }
}
