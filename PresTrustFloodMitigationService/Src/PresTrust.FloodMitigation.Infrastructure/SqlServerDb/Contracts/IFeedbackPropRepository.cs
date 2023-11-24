namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IFeedbackPropRepository
{

    /// <summary>
    /// Procedure to fetch application's prop feedback for a given application status or all
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="correctionStatus"></param>
    /// <returns></returns>
    Task<List<FloodPropertyFeedbackEntity>> GetPropertyFeedbackAsync(int applicationId, string pamsPin, string correctionStatus = "");
    // <summary>
    /// Save Feedback.
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns>Returns Feedback.</returns>
    Task<FloodPropertyFeedbackEntity> SavePropertyFeedbackAsync(FloodPropertyFeedbackEntity feedback);
    /// <summary>
    /// Delete Feedback
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    Task DeletePropertyFeedbackAsync(FloodPropertyFeedbackEntity feedback);

    Task MarkPropertyFeedbackAsReadAsync(List<int> FeedbackIds);
    Task RequestForPropertyCorrectionAsync(int applicationId, string pamsPin);
    /// <summary>
    /// Response to request for application correction
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    Task ResponseToRequestForPropertyCorrectionAsync(int applicationId, string pamsPin, int sectionId);
}
