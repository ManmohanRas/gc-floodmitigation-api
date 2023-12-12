namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteCountyUserRoleCommandValidator : AbstractValidator<DeleteCountyUserRoleCommand>
{
    public DeleteCountyUserRoleCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotNull().NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(command => command.Role)
            .NotNull().NotEmpty().WithMessage("Role is required.")
            .Must(x => ValidRole(x)).WithMessage("Invalid agency user role.");
    }

    /// <summary>
    /// Check if a given role is valid
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool ValidRole(string role)
    {
        bool result = false;

        if (string.IsNullOrEmpty(role))
            return true;

        switch (role)
        {
            case IdentityRoles.FLOOD_PROGRAM_ADMIN:
            case IdentityRoles.FLOOD_PROGRAM_COMMITTEE:
            case IdentityRoles.FLOOD_PROGRAM_EDITOR:
            case IdentityRoles.FLOOD_PROGRAM_READONLY:
           // case IdentityRoles.FLOOD_PROGRAM_CONSULTANT:
                result = true;
                break;
            default:
                break;
        }

        return result;
    }
}
