namespace PresTrust.FloodMitigation.Domain.Enums;

public enum EmailTemplateCodeTypeEnum
{
    /// <summary>
    /// Default Email Template Code Type
    /// </summary>
    NONE = 0,
    /// <summary>
    /// 
    /// </summary>
    CHANGE_STATUS_FROM_DOI_DRAFT_TO_DOI_SUBMITTED,

    /// <summary>
    /// 
    /// </summary>
    CHANGE_STATUS_FROM_DOI_SUBMITTED_TO_DOI_APPROVED,

    /// <summary>
    /// 
    /// </summary>
    CHANGE_STATUS_FROM_DOI_APPROVED_TO_SUBMITTED,

    /// <summary>
    /// 
    /// </summary>
    CHANGE_STATUS_FROM_SUBMITTED_TO_IN_REVIEW,

    /// <summary>
    /// 
    /// </summary>
    CHANGE_STATUS_FROM_IN_REVIEW_TO_REJECTED,

    /// <summary>
    /// 
    /// </summary>
    CHANGE_STATUS_FROM_IN_REVIEW_TO_ACTIVE,

    /// <summary>
    /// 
    /// </summary>
    CHANGE_STATUS_FROM_ACTIVE_TO_WITHDRAWN,

    /// <summary>
    /// 
    /// </summary>
    CHANGE_STATUS_FROM_ACTIVE_TO_CLOSED,

    /// <summary>
    /// Email Template Code Type for FEEDBACK_EMAIL
    /// </summary>
    FEEDBACK_EMAIL,

    /// <summary>
    /// Email Template Code Type for FEEDBACK_RESPONSE_EMAIL
    /// </summary>
    FEEDBACK_RESPONSE_EMAIL,

    /// <summary>
    /// Email Template Code Type for CHANGE_PROPERTY_STATUS_FROM_PENDING_TO_APPROVED
    /// </summary>
    CHANGE_PROPERTY_STATUS_FROM_PENDING_TO_APPROVED,

    /// <summary>
    /// Email Template Code Type for CHANGE_PROPERTY_STATUS_FROM_PENDING_TO_APPROVED
    /// </summary>
    CHANGE_PROPERTY_STATUS_FROM_APPROVED_TO_PRESERVED,

    /// <summary>
    /// Email Template Code Type for PROPERTY_SCHEDULE_CLOSING
    /// </summary>
    PROPERTY_SCHEDULE_CLOSING,//NEW 

    /// <summary>
    /// Email Template Code Type for SUBMIT_SOFTCOST
    /// </summary>
    SUBMIT_SOFTCOST,

    /// <summary>
    /// Email Template Code Type for APPROVE_SOFTCOST
    /// </summary>
    APPROVE_SOFTCOST
}
