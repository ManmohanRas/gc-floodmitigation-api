namespace PresTrust.FloodMitigation.Domain.Enums;

public enum ApplicationDocumentTypeEnum
{

    /// <summary>
    /// DocumentType Type for None
    /// </summary>
    NONE = 0,

    /// <summary>
    /// DocumentType Type for LOE - OVERVIEW TAB
    /// </summary>
    LOE = 1,

    /// <summary>
    /// DocumentType Type for FEMA - OVERVIEW TAB
    /// </summary>
    FEMA = 2,

    /// <summary>
    /// DocumentType Type for GREEN_ACRES - OVERVIEW TAB
    /// </summary>
    GREEN_ACRES = 3,

    /// <summary>
    /// DocumentType Type for BLUE_ACRES - OVERVIEW TAB
    /// </summary>
    BLUE_ACRES = 4,

    /// <summary>
    /// DocumentType Type for OTHER_FUNDING_AGENCY - OVERVIEW TAB
    /// </summary>
    OTHER_FUNDING_AGENCY = 5,

    /// <summary>
    /// DocumentType Type for OTHER_FUNDING_AGENCY - OTHER DOCUMENTS TAB
    /// </summary>
    APPLICATION_CHECKLIST = 6,

    /// <summary>
    /// DocumentType Type for PUBLIC_HEARING_CERTIFICATE - OTHER DOCUMENTS TAB
    /// </summary>
    PUBLIC_HEARING_CERTIFICATE = 7,

    /// <summary>
    /// DocumentType Type for MINUTES_FROM_PUBLIC_HEARING - OTHER DOCUMENTS TAB
    /// </summary>
    MINUTES_FROM_PUBLIC_HEARING = 8,

    /// <summary>
    /// DocumentType Type for MUNICIPAL_RESOLUTION_OF_SUPPORT - OTHER DOCUMENTS TAB
    /// </summary>
    MUNICIPAL_RESOLUTION_OF_SUPPORT = 9,

    /// <summary>
    /// DocumentType Type for NON_COUNTY_AGENCY_RESOLUTION - OTHER DOCUMENTS TAB
    /// </summary>
    NON_COUNTY_AGENCY_RESOLUTION = 10,

    /// <summary>
    /// DocumentType Type for OTHER_DOCUMENTS - OTHER DOCUMENTS TAB
    /// </summary>
    OTHER_DOCUMENTS = 11,

    /// <summary>
    /// DocumentType Type for FMC_PRELIMINARY_APPROVAL_RESOLUTION - OTHER DOCUMENTS TAB
    /// </summary>
    FMC_PRELIMINARY_APPROVAL_RESOLUTION = 12,

    /// <summary>
    /// DocumentType Type for PUBLIC_HEARING_CERTIFICATE - OTHER DOCUMENTS TAB
    /// </summary>
    BCC_PRELIMINARY_APPROVAL_RESOLUTION =  13,

    /// <summary>
    /// DocumentType Type for CONGRATULATIONS_LETTER_TO_HOME_OWNER - OTHER DOCUMENTS TAB
    /// </summary>
    CONGRATULATIONS_LETTER_TO_HOME_OWNER = 14,

    /// <summary>
    /// DocumentType Type for NOTIFICATION_OF_APPROVAL_AND_PROCEDURES_LETTER - OTHER DOCUMENTS TAB
    /// </summary>
    NOTIFICATION_OF_APPROVAL_AND_PROCEDURES_LETTER = 15,

    /// <summary>
    /// DocumentType Type for PROJECT_AREA_FUNDS_EXPIRATION_REQUEST - OTHER DOCUMENTS TAB
    /// </summary>
    PROJECT_AREA_FUNDS_EXPIRATION_REQUEST = 16,
    /// <summary>
    /// DocumentType Type for COMMISSIONER_RESOLUTION - ADMINT DETAILS TAB
    /// </summary>
    COMMISSIONER_RESOLUTION = 17,
    /// <summary>
    /// DocumentType Type for FIRST_COMMISSIONER_RESOLUTION - ADMINT DETAILS TAB
    /// </summary>
    FIRST_COMMISSIONER_RESOLUTION = 18,
    /// <summary>
    /// DocumentType Type for SECOND_COMMISSIONER_RESOLUTION - ADMINT DETAILS TAB
    /// </summary>
    SECOND_COMMISSIONER_RESOLUTION = 19,
    /// <summary>
    /// DocumentType Type for CORE_APPLICATION_REPORT - ADMINT DETAILS TAB
    /// </summary>
    CORE_APPLICATION_REPORT = 20,
    /// <summary>
    /// DocumentType Type for CORE_REVIEW_REPORT - ADMINT DETAILS TAB
    /// </summary>
    CORE_REVIEW_REPORT = 21,
    /// <summary>
    /// DocumentType Type for PROJECT_AREA_APPLICATION_MAP - ADMINT DETAILS TAB
    /// </summary>
    PROJECT_AREA_APPLICATION_MAP = 22,
    /// <summary>
    /// DocumentType Type for CAF_CLOSE_OUT_SUMMARY - ADMINT DETAILS TAB
    /// </summary>
    CAF_CLOSE_OUT_SUMMARY = 23,


}
