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
    APPROVE_SOFTCOST,

    /// <summary>
    /// Email Template Code Type for REMINDER_ABOUT_EXPIRATION_OF_FIRST_GRANT_EXTENSION
    /// </summary>
    REMINDER_ABOUT_EXPIRATION_OF_FIRST_GRANT_EXTENSION,

    /// <summary>
    /// Email Template Code Type for REMINDER_ABOUT_EXPIRATION_OF_SECOND_GRANT_EXTENSION
    /// </summary>
    REMINDER_ABOUT_EXPIRATION_OF_SECOND_GRANT_EXTENSION,

    /// <summary>
    /// Email Template Code Type for DUE_DILIGIENCE_DOCUMENTS
    /// </summary>
    DUE_DILIGIENCE_DOCUMENTS,

    /// <summary>
    /// Email Template Code Type for SOFTCOST_REIMBURSEMENT_INFORMATION
    /// </summary>
    SOFTCOST_REIMBURSEMENT_INFORMATION,

    /// <summary>
    /// Email Template Code Type for FLAP_UPDATE
    /// </summary>
    FLAP_UPDATE,

    /// <summary>
    /// Email Template Code Type for PROJECT_AREA_EXPIRATION_REMINDER
    /// </summary>
    PROJECT_AREA_EXPIRATION_REMINDER,

    /// <summary>
    /// Email Template Code Type for GRANT_EXPIRATION_REMINDER
    /// </summary>
    GRANT_EXPIRATION_REMINDER,

    /// <summary>
    /// Email Template Code Type for DEMOLITION_REMINDER
    /// </summary>
    DEMOLITION_REMINDER
}
