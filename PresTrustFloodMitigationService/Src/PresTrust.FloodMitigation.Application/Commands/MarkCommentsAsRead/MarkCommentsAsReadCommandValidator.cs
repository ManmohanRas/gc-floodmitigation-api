namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkCommentsAsReadCommandValidator : AbstractValidator<MarkCommentsAsReadCommand>
{
    public MarkCommentsAsReadCommandValidator()
    {
        RuleFor(command => command.CommentIds)
          .Must(o => o.Count > 0).WithMessage("Not a valid Comment Id");
    }
}
