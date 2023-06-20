namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkFeedbacksAsReadCommandValidator : AbstractValidator<MarkFeedbacksAsReadCommand>
{
    public MarkFeedbacksAsReadCommandValidator()
    {
        RuleFor(command => command.FeedbackIds)
          .Must(o => o.Count > 0).WithMessage("Not a valid Feedback Id");
    }
}
