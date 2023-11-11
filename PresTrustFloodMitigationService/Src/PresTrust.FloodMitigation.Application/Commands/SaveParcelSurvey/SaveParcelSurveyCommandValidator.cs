namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelSurveyCommandValidator : AbstractValidator<SaveParcelSurveyCommand>
{
    public SaveParcelSurveyCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
        RuleFor(command => command.PamsPin)
                .NotEmpty()
                .WithMessage("Not a valid PamsPin.");
    }
}
