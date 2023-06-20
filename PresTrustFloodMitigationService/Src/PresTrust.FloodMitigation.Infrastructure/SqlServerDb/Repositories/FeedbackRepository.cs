using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Feedback;

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
    public async Task<IEnumerable<FlmitigFeedbackEntity>> GetFeedbacksAsync(int applicationId, string correctionStatus = "")
    {
        IEnumerable<FlmitigFeedbackEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFeedbackSqlCommand();
        results = await conn.QueryAsync<FlmitigFeedbackEntity>(sqlCommand.ToString(),
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
    public async Task<FlmitigFeedbackEntity> SaveAsync(FlmitigFeedbackEntity feedback)
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
    private async Task<FlmitigFeedbackEntity> CreateAsync(FlmitigFeedbackEntity feedback)
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
    private async Task<FlmitigFeedbackEntity> UpdateAsync(FlmitigFeedbackEntity feedback)
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
    public async Task DeleteAsync(FlmitigFeedbackEntity feedback)
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
}

