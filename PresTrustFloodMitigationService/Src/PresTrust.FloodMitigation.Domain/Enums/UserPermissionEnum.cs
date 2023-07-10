namespace PresTrust.FloodMitigation.Domain.Enums;

/// <summary>
/// Flood Mitigation User Permissions
/// </summary>
public enum UserPermissionEnum
{
    /// <summary>
    /// Permission Type for None
    /// </summary>
    NONE = 0,
    /// <summary>
    /// Permission to view declaration of intent details
    /// </summary>
    VIEW_DECLARATION_OF_INTENT_SECTION = 1,
    /// <summary>
    /// Permission to edit declaration of intent details
    /// </summary>
    EDIT_DECLARATION_OF_INTENT_SECTION = 2,
    /// <summary>
    /// Permission to view property details
    /// </summary>
    VIEW_ROLES_SECTION = 3,
    /// <summary>
    /// Permission to edit property details
    /// </summary>
    EDIT_ROLES_SECTION = 4,
    /// <summary>
    /// Permission to view assigned users
    /// </summary>
    VIEW_OVERVIEW_SECTION = 5,
    /// <summary>
    /// Permission to edit assigned users
    /// </summary>
    EDIT_OVERVIEW_SECTION = 6,
    /// <summary>
    /// Permission to view budget details
    /// </summary>
    VIEW_PROJECTAREA_SECTION = 7,
    /// <summary>
    /// Permission to edit budget details
    /// </summary>
    EDIT_PROJECTAREA_SECTION = 8,
    /// <summary>
    /// Permission to view scope of work details
    /// </summary>
    VIEW_FINANCE_SECTION = 9,
    /// <summary>
    /// Permission to edit scope of work details
    /// </summary>
    EDIT_FINANCE_SECTION = 10,
    /// <summary>
    /// Permission to view public benefit details
    /// </summary>
    VIEW_SIGNATORY_SECTION = 11,
    /// <summary>
    /// Permission to edit view public benefit details
    /// </summary>
    EDIT_SIGNATORY_SECTION = 12,
    /// <summary>
    /// Permission to view other docs details
    /// </summary>
    VIEW_OTHER_DOCS_SECTION = 13,
    /// <summary>
    /// Permission to edit other docs details
    /// </summary>
    EDIT_OTHER_DOCS_SECTION = 14,
    /// <summary>
    /// Permission to view document check lists
    /// </summary>
    VIEW_ADMIN_DOC_CHK_LIST_SECTION = 15,
    /// <summary>
    /// Permission to edit document check lists
    /// </summary>
    EDIT_ADMIN_DOC_CHK_LIST_SECTION = 16,
    /// <summary>
    /// Permission to view admin details section
    /// </summary>
    VIEW_ADMIN_DETAILS_SECTION = 17,
    /// <summary>
    /// Permission to edit admin details section
    /// </summary>
    EDIT_ADMIN_DETAILS_SECTION = 18,
    /// <summary>
    /// Permission to view admin contacts section
    /// </summary>
    VIEW_ADMIN_CONTACTS_SECTION = 19,
    /// <summary>
    /// Permission to edit admin contacts section
    /// </summary>
    EDIT_ADMIN_CONTACTS_SECTION = 20,
    /// <summary>
    /// Permission to view admin release of funds section
    /// </summary>
    VIEW_ADMIN_RELEASE_OF_FUNDS_SECTION = 21,
    /// <summary>
    /// Permission to edit admin release of funds section
    /// </summary>
    EDIT_ADMIN_RELEASE_OF_FUNDS_SECTION = 22,
    /// <summary>
    /// Permission to view comments
    /// </summary>
    VIEW_COMMENTS = 101,
    /// <summary>
    /// Permission to edit feedback
    /// </summary>
    EDIT_COMMENTS = 102,
    /// <summary>
    /// Permission to delete comments
    /// </summary>
    DELETE_COMMENTS = 103,
    /// <summary>
    /// Permission to view feedback
    /// </summary>
    VIEW_FEEDBACK = 104,
    /// <summary>
    /// Permission to edit feedback
    /// </summary>
    EDIT_FEEDBACK = 105,
    /// <summary>
    /// Permission to delete feedbacks
    /// </summary>
    DELETE_FEEDBACKS = 106,
    /// <summary>
    /// Permission to request for an application correction
    /// </summary>
    REQUEST_FOR_AN_APPLICATION_CORRECTION  = 107,
    /// <summary>
    /// Permission to respond to the request for an application correction
    /// </summary>
    RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION = 108,
    /// <summary>
    /// Permission to save document
    /// </summary>
    SAVE_DOCUMENT = 109,
    /// <summary>
    /// Permission to delete document
    /// </summary>
    DELETE_DOCUMENT = 110,
    /// <summary>
    /// Permission to create application
    /// </summary>
    CREATE_APPLICATION = 201,
    /// <summary>
    /// Permission to submit declaration
    /// </summary>
    SUBMIT_DECLARATION_OF_INTENT = 202,
    /// <summary>
    /// Permission to approve declaration
    /// </summary>
    APPROVE_DECLARATION_OF_INTENT = 203,
    /// <summary>
    /// Permission to submit application
    /// </summary>
    SUBMIT_APPLICATION = 204,
    /// <summary>
    /// Permission to review application
    /// </summary>
    REVIEW_APPLICATION = 205,
    /// <summary>
    /// Permission to activate application
    /// </summary>
    ACTIVATE_APPLICATION = 206,
    /// <summary>
    /// Permission to close application
    /// </summary>
    CLOSE_APPLICATION = 207,
    /// <summary>
    /// Permission to reject application
    /// </summary>
    REJECT_APPLICATION = 208,
    /// <summary>
    /// Permission to withdraw application
    /// </summary>
    WITHDRAW_APPLICATION = 209,
    /// <summary>
    /// Permission to reinitate application
    /// </summary>
    REINITIATE_APPLICATION = 210
}
