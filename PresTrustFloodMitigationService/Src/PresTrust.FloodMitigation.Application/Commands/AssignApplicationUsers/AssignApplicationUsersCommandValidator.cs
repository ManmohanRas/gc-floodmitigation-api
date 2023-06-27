namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class validates command input
/// </summary>
/// <remarks>
/// Returns BadRequest Response if any failures occured
/// </remarks>
public class AssignApplicationUsersCommandValidator: AbstractValidator<AssignApplicationUsersCommand>
{
    public AssignApplicationUsersCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
