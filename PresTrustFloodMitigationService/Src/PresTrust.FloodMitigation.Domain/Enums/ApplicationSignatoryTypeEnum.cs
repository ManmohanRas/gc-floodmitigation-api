namespace PresTrust.FloodMitigation.Domain.Enums;

/// <summary>
/// Agency Types
/// </summary>
public enum ApplicationSignatoryTypeEnum
{
    /// <summary>
    /// Signatory Type for None
    /// </summary>
    NONE = 0,
    /// <summary>
    /// Signatory Type for CERTIFY_APPLICATION
    /// </summary>
    CERTIFY_APPLICATION = 1,
    /// <summary>
    /// Signatory Type for PROFESSIONAL_SERVICE_CHECKLIST
    /// </summary>
    PROFESSIONAL_SERVICE_CHECKLIST = 2,
    /// <summary>
    /// Signatory Type for PLANNING_DOCUMENT_CHECKLIST
    /// </summary>
    PLANNING_DOCUMENT_CHECKLIST = 3,
    /// <summary>
    /// Signatory Type for DECLARATION_OF_INTENT
    /// </summary>
    DECLARATION_OF_INTENT = 4,
    /// <summary>
    /// Signatory Type for OWNERS_ASSURANCE_DIGITAL
    /// </summary>
    OWNERS_ASSURANCE_DIGITAL = 5,
}
