namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationFeedbackRepository
{
    /// <summary>
    /// Procedure to fetch application's feedback for a given application status or all
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="correctionStatus"></param>
    /// <returns></returns>
    Task<List<FloodApplicationFeedbackEntity>> GetFeedbacksAsync(int applicationId, string correctionStatus = "");
    // <summary>
    /// Save Feedback.
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns>Returns Feedback.</returns>
    Task<FloodApplicationFeedbackEntity> SaveAsync(FloodApplicationFeedbackEntity feedback);
    /// <summary>
    /// Delete Feedback
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    Task DeleteAsync(FloodApplicationFeedbackEntity feedback);

    Task MarkFeedbacksAsReadAsync(List<int> FeedbackIds);
    Task RequestForApplicationCorrectionAsync(int applicationId);
    /// <summary>
    /// Response to request for application correction
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    Task ResponseToRequestForApplicationCorrectionAsync(int applicationId, int sectionId);
}
