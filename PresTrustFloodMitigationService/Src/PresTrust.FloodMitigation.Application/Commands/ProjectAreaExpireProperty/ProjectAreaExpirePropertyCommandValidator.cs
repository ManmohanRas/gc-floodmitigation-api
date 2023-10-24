namespace PresTrust.FloodMitigation.Application.Commands;

public class ProjectAreaExpirePropertyCommandValidator : AbstractValidator<ProjectAreaExpirePropertyCommand>
{
    public ProjectAreaExpirePropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
    }
}
