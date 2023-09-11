using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;
public class FeedbackRepository : IFeedbackRepository
{
    #region " Members ... "

    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    #endregion

    #region " ctor ... "

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="systemParamConfigOptions"></param>
    public FeedbackRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion

    /// <summary>
    /// Procedure to fetch application's feedback for a given application status or all
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="correctionStatus"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FloodFeedbackEntity>> GetFeedbacksAsync(int applicationId, string correctionStatus = "")
    {
        IEnumerable<FloodFeedbackEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFeedbackSqlCommand();
        results = await conn.QueryAsync<FloodFeedbackEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId, @p_CorrectionStatus = correctionStatus });

        return results;
    }

    /// <summary>
    /// Save Feedback.
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    public async Task<FloodFeedbackEntity> SaveAsync(FloodFeedbackEntity feedback)
    {
        if (feedback.Id > 0)
            return await UpdateAsync(feedback);
        else
            return await CreateAsync(feedback);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    private async Task<FloodFeedbackEntity> CreateAsync(FloodFeedbackEntity feedback)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFeedbackSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = feedback.ApplicationId,
                @p_SectionId = feedback.SectionId,
                @p_Feedback = feedback.Feedback,
                @p_RequestForCorrection = feedback.RequestForCorrection,
                @p_CorrectionStatus = feedback.CorrectionStatus,
                @p_MarkRead = feedback.MarkRead,
                @p_LastUpdatedBy = feedback.LastUpdatedBy,
            });

        feedback.Id = id;

        return feedback;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    private async Task<FloodFeedbackEntity> UpdateAsync(FloodFeedbackEntity feedback)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFeedbackSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = feedback.Id,
                @p_ApplicationId = feedback.ApplicationId,
                @p_SectionId = feedback.SectionId,
                @p_Feedback = feedback.Feedback,
                @p_RequestForCorrection = feedback.RequestForCorrection,
                @p_CorrectionStatus = feedback.CorrectionStatus,
                @p_LastUpdatedBy = feedback.LastUpdatedBy
            });

        return feedback;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    public async Task DeleteAsync(FloodFeedbackEntity feedback)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteFeedbackSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = feedback.Id,
                @p_ApplicationId = feedback.ApplicationId,
            });
    }
     public async Task MarkFeedbacksAsReadAsync(List<int> feedbackIds)
     {
            using var conn = context.CreateConnection();
            var sqlCommand = new MarkFeedbacksAsReadSqlCommand();
            await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_FeedbackIds = feedbackIds
            });
     }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    public async Task RequestForApplicationCorrectionAsync(int applicationId)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new RequestForPropertyCorrectionCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = applicationId,
                @p_CorrectionStatus = ApplicationCorrectionStatusEnum.REQUEST_SENT.ToString(),
            });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    public async Task ResponseToRequestForApplicationCorrectionAsync(int applicationId, int sectionId)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new ResponseToRequestForPropertyCorrectionCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = applicationId,
                @p_SectionId = sectionId,
                @p_CorrectionStatus = ApplicationCorrectionStatusEnum.RESPONSE_RECEIVED.ToString(),
            });
    }
}

