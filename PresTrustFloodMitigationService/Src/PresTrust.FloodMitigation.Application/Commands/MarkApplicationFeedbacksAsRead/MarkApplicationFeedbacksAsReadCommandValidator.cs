namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkApplicationFeedbacksAsReadCommandValidator : AbstractValidator<MarkApplicationFeedbacksAsReadCommand>
{
    public MarkApplicationFeedbacksAsReadCommandValidator()
    {
        RuleFor(command => command.FeedbackIds)
          .Must(o => o.Count > 0).WithMessage("Not a valid Feedback Id");
    }
}
