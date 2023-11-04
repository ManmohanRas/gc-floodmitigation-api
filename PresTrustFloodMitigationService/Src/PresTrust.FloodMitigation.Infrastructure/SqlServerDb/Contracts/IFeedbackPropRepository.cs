namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IFeedbackPropRepository
{

    /// <summary>
    /// Procedure to fetch application's prop feedback for a given application status or all
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="correctionStatus"></param>
    /// <returns></returns>
    Task<List<FloodPropertyFeedbackEntity>> GetPropFeedbackAsync(int applicationId, string Pamspin);
    // <summary>
    /// Save Feedback.
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns>Returns Feedback.</returns>
    Task<FloodPropertyFeedbackEntity> SavePropFeedbackAsync(FloodPropertyFeedbackEntity feedback);
    /// <summary>
    /// Delete Feedback
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    Task DeletePropFeedbackAsync(FloodPropertyFeedbackEntity feedback);

    Task MarkPropFeedbackAsReadAsync(List<int> FeedbackIds);
    Task RequestForPropertyCorrectionAsync(int applicationId);
    /// <summary>
    /// Response to request for application correction
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    Task ResponseToRequestForPropertyCorrectionAsync(int applicationId, int sectionId);
}
