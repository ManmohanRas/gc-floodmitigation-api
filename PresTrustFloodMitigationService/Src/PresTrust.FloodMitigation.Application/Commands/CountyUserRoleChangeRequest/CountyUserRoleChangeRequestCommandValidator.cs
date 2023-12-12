using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Commands;

public class CountyUserRoleChangeRequestCommandValidator : AbstractValidator<CountyUserRoleChangeRequestCommand>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public CountyUserRoleChangeRequestCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotNull().NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(command => command.Role)
            .Must(x => ValidRole(x)).WithMessage("Invalid agency user role.");

        RuleFor(command => command.NewRole)
            .NotNull().NotEmpty().WithMessage("New Role is required.")
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
                result = true;
                break;
            default:
                break;
        }

        return result;
    }
}
