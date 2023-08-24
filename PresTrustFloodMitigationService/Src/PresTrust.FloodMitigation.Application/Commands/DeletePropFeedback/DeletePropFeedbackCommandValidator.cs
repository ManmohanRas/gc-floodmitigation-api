namespace PresTrust.FloodMitigation.Application.Commands;
public class DeletePropFeedbackCommandValidator : AbstractValidator<DeletePropFeedbackCommand>
{

    /// <summary>
    /// create rules for attributes
    /// </summary>
    public DeletePropFeedbackCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0)
            .WithMessage("Not a valid Application Id");

        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid Feedback Id");

        RuleFor(command => command.Pamspin)
           .NotEmpty()
           .WithMessage("Not a valid Pamspin");
    }
}
