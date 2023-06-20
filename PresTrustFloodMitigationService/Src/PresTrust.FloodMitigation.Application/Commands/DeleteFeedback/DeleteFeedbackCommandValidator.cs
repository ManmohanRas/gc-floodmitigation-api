namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class validates command input
/// </summary>
/// <remarks>
/// Returns BadRequest Response if any failures occured
/// </remarks>
public class DeleteFeedbackCommandValidator : AbstractValidator<DeleteFeedbackCommand>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public DeleteFeedbackCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0)
            .WithMessage("Not a valid Application Id");

        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid Feedback Id");
    }
}
