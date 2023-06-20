namespace PresTrust.FloodMitigation.Domain.Enums;

public enum ApplicationCorrectionStatusEnum
{
    /// <summary>
    /// Application Correction Status for None
    /// </summary>
    NONE = 0,
    /// <summary>
    /// Application Correction Status for Pending
    /// </summary>
    PENDING = 1,
    /// <summary>
    /// Application Correction Status for Request Sent
    /// </summary>
    REQUEST_SENT = 2,
    /// <summary>
    /// Application Correction Status for Response Received
    /// </summary>
    RESPONSE_RECEIVED = 3,
}
