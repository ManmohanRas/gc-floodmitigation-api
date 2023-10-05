namespace PresTrust.FloodMitigation.Domain.Enums;

public enum EmailTemplateCodeTypeEnum
{
    /// <summary>
    /// Default Email Template Code Type
    /// </summary>
    NONE = 0,

    /// <summary>
    /// Email Template Code Type for CHANGE_STATUS_FROM_DOI_DRAFT_TO_DOI_SUBMITTED
    /// </summary>
    CHANGE_STATUS_FROM_DOI_DRAFT_TO_DOI_SUBMITTED,

    /// <summary>
    /// Email Template Code Type for CHANGE_STATUS_FROM_DOI_SUBMITTED_TO_DOI_APPROVED
    /// </summary>
    CHANGE_STATUS_FROM_DOI_SUBMITTED_TO_DOI_APPROVED,

    /// <summary>
    /// Email Template Code Type for CHANGE_STATUS_FROM_DOI_SUBMITTED_TO_REJECTED
    /// </summary>
    CHANGE_STATUS_FROM_DOI_SUBMITTED_TO_REJECTED,
}
