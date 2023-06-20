namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFeedbackCommandValidator : AbstractValidator<SaveFeedbackCommand>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public SaveFeedbackCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");

        RuleFor(command => command.Feedback)
            .NotNull().NotEmpty().WithMessage("Feedback cannot be empty");

        RuleFor(command => command.Section)
            .NotNull().NotEmpty().WithMessage("Application section must be provided")
            .Must(appSection => EnumUtils.TryParseWithMemberName<ApplicationSectionEnum>(appSection, out _)).WithMessage("Not a valid application section");
    }
}
