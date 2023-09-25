namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationCommentCommandValidator:AbstractValidator<DeleteApplicationCommentCommand>
{
    public DeleteApplicationCommentCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
           .GreaterThan(0)
           .WithMessage("Not a valid ApplicationId");

        RuleFor(query => query.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid CommentId");
    }
}
