namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkApplicationCommentsAsReadCommandValidator : AbstractValidator<MarkApplicationCommentsAsReadCommand>
{
    public MarkApplicationCommentsAsReadCommandValidator()
    {
        RuleFor(command => command.CommentIds)
          .Must(o => o.Count > 0).WithMessage("Not a valid Comment Id");
    }
}
