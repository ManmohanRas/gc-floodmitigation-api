namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class validates command input
/// </summary>
/// <remarks>
/// Returns BadRequest Response if any failures occured
/// </remarks>
public class DeleteApplicationFeedbackCommandValidator : AbstractValidator<DeleteApplicationFeedbackCommand>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public DeleteApplicationFeedbackCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0)
            .WithMessage("Not a valid Application Id");

        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid Feedback Id");
    }
}
