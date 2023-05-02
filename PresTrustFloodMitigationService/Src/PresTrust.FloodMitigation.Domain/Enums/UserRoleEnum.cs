namespace PresTrust.FloodMitigation.Domain.Enums
{
    /// <summary>
    /// Pres Trust User's security roles
    /// </summary>
    public enum UserRoleEnum
    {
        /// <summary>
        /// UserRole Type for None
        /// </summary>
        NONE = 0,
        /// <summary>
        /// Pres Trust User's role for System Admin
        /// </summary>
        SYSTEM_ADMIN = 1,
        /// <summary>
        /// Pres Trust User's role for System Support
        /// </summary>
        SYSTEM_SUPPORT = 2,
        /// <summary>
        /// Pres Trust User's role for Program Admin
        /// </summary>
        PROGRAM_ADMIN = 3,
        /// <summary>
        /// Pres Trust User's role for Program Editor
        /// </summary>
        PROGRAM_EDITOR = 4,
        /// <summary>
        /// Pres Trust User's role for Program Committee
        /// </summary>
        PROGRAM_COMMITTEE = 5,
        /// <summary>
        /// Pres Trust User's role for Program Readonly
        /// </summary>
        PROGRAM_READONLY = 6,
        /// <summary>
        /// Pres Trust User's role for Agency Admin
        /// </summary>
        AGENCY_ADMIN = 7,
        /// <summary>
        /// Pres Trust User's role for Agency Editor
        /// </summary>
        AGENCY_EDITOR = 8,
        /// <summary>
        /// Pres Trust User's role for Agency Signatory
        /// </summary>
        AGENCY_SIGNATORY = 9,
        /// <summary>
        /// Pres Trust User's role for Agency ReadOnly
        /// </summary>
        AGENCY_READONLY = 10,
        /// <summary>
        /// Pres Trust User's role for Agency Architect
        /// </summary>
        AGENCY_ARCHITECT = 11,
        /// <summary>
        /// Pres Trust User's role for Program Consultant
        /// </summary>
        PROGRAM_CONSULTANT = 12
    }
}
