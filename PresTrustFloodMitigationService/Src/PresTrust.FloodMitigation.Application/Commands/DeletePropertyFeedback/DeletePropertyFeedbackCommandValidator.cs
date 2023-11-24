namespace PresTrust.FloodMitigation.Application.Commands;
public class DeletePropertyFeedbackCommandValidator : AbstractValidator<DeletePropertyFeedbackCommand>
{

    /// <summary>
    /// create rules for attributes
    /// </summary>
    public DeletePropertyFeedbackCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0)
            .WithMessage("Not a valid Application Id");

        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid Feedback Id");

        RuleFor(command => command.PamsPin)
           .NotEmpty()
           .WithMessage("Not a valid PamsPin");
    }
}
