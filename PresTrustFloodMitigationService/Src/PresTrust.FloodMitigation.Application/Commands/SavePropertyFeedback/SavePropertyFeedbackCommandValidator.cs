namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyFeedbackCommandValidator : AbstractValidator<SavePropertyFeedbackCommand>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public SavePropertyFeedbackCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");

        RuleFor(command => command.Feedback)
            .NotNull().NotEmpty().WithMessage("Feedback cannot be empty");

        RuleFor(command => command.Section)
            .NotNull().NotEmpty().WithMessage("Application section must be provided")
            .Must(propSection => EnumUtils.TryParseWithMemberName<PropertySectionEnum>(propSection, out _)).WithMessage("Not a valid application section");
    }
}
