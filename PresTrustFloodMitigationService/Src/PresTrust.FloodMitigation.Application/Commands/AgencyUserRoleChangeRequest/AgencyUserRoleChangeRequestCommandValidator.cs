namespace PresTrust.FloodMitigation.Application.Commands
{
    public class AgencyUserRoleChangeRequestCommandValidator : AbstractValidator<AgencyUserRoleChangeRequestCommand>
    {
        /// <summary>
        /// create rules for attributes
        /// </summary>
        public AgencyUserRoleChangeRequestCommandValidator()
        {
            RuleFor(command => command.AgencyId)
               .GreaterThan(0).WithMessage("Not a valid Agency Id");

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
                case IdentityClaimTypes.FLOOD_AGENCY_ADMIN:
                case IdentityClaimTypes.FLOOD_AGENCY_EDITOR:
                case IdentityClaimTypes.FLOOD_AGENCY_READONLY:
                case IdentityClaimTypes.FLOOD_AGENCY_SIGNATURE:
                    result = true;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
