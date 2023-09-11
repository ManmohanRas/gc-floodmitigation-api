namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    public DeleteContactCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
           .GreaterThan(0)
           .WithMessage("Not a valid ApplicationId");

        RuleFor(query => query.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid CommentId");
    }
}
