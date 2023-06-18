namespace PresTrust.FloodMitigation.Domain.Enums;

/// <summary>
/// Pres Trust User's Status
/// </summary>
public enum UserStatusEnum
{
    /// <summary>
    /// Pres Trust User status for Waiting for Confirmation
    /// </summary>
    WaitingForConfirmation = 1,
    /// <summary>
    /// Pres Trust User status for Waiting for Active
    /// </summary>
    Active = 2,
    /// <summary>
    /// Pres Trust User status for Waiting for Deactive
    /// </summary>
    Deactive = 3
}
