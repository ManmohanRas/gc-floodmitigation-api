namespace PresTrust.FloodMitigation.Domain.Constants;

public partial class FloodMitigationDomainConstants
{
    /// <summary>
    /// Class to hold constants for Preservation Trust Claim Types
    /// </summary>
    public static class IdentityClaimTypes
    {
        /// <summary>
        /// Constant to hold Claim Type for GivenName 
        /// </summary>
        public const string NAME = "given_name";
        /// <summary>
        /// Constant to hold Claim Type for Email 
        /// </summary>
        public const string EMAIL = "email";
        /// <summary>
        /// Constant to hold Claim Type for FloodAgencyAdmin
        /// </summary>
        public const string FLOOD_AGENCY_ADMIN = "flood_agencyadmin";
        /// <summary>
        /// Constant to hold Claim Type for FloodAgencySignature
        /// </summary>
        public const string FLOOD_AGENCY_SIGNATURE = "flood_agencysignature";
        /// <summary>
        /// Constant to hold Claim Type for FloodAgencyEditor
        /// </summary>
        public const string FLOOD_AGENCY_EDITOR = "flood_agencyeditor";
        /// <summary>
        /// Constant to hold Claim Type for FloodAgencyReadOnly
        /// </summary>
        public const string FLOOD_AGENCY_READONLY = "flood_agencyreadonly";
    }
}
