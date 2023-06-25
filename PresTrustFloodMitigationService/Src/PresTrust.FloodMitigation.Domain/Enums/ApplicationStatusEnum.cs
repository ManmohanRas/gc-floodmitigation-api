namespace PresTrust.FloodMitigation.Domain.Enums;

public enum ApplicationStatusEnum
{
    /// <summary>
    /// Application Status Type for None
    /// </summary>
    NONE = 0,

    /// <summary>
    /// Application Status Type for DOI Draft
    /// </summary>
    DECLARATION_OF_INTENT_DRAFT = 1,

    /// <summary>
    /// Application Status Type for DOI Submitted
    /// </summary>
    DECLARATION_OF_INTENT_SUBMITTED = 2,

    /// <summary>
    /// Application Status Type for Draft
    /// </summary>
    DRAFT = 3,

    /// <summary>
    /// Application Status Type for Submitted
    /// </summary>
    SUBMITTED = 4,

    /// <summary>
    /// Application Status Type for In Review
    /// </summary>
    IN_REVIEW = 5,

    /// <summary>
    /// Application Status Type for Active
    /// </summary>
    ACTIVE = 6,

    /// <summary>
    /// Application Status Type for Closed
    /// </summary>
    CLOSED = 7,

    /// <summary>
    /// Application Status Type for Rejected
    /// </summary>
    REJECTED = 8,

    /// <summary>
    /// Application Status Type for Withdrawn
    /// </summary>
    WITHDRAWN = 9
}
