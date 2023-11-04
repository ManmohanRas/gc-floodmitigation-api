using PresTrust.FloodMitigation.Domain.Enums;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Feedback;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;
public class ApplicationFeedbackRepository : IApplicationFeedbackRepository
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
    public ApplicationFeedbackRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
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
    public async Task<List<FloodApplicationFeedbackEntity>> GetFeedbacksAsync(int applicationId, string correctionStatus = "")
    {
        List<FloodApplicationFeedbackEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationFeedbackSqlCommand();
        results = (await conn.QueryAsync<FloodApplicationFeedbackEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId, @p_CorrectionStatus = correctionStatus })).ToList();

        return results ?? new();
    }

    /// <summary>
    /// Save Feedback.
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    public async Task<FloodApplicationFeedbackEntity> SaveAsync(FloodApplicationFeedbackEntity feedback)
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
    private async Task<FloodApplicationFeedbackEntity> CreateAsync(FloodApplicationFeedbackEntity feedback)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationFeedbackSqlCommand();
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
    private async Task<FloodApplicationFeedbackEntity> UpdateAsync(FloodApplicationFeedbackEntity feedback)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationFeedbackSqlCommand();
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
    public async Task DeleteAsync(FloodApplicationFeedbackEntity feedback)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteApplicationFeedbackSqlCommand();
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
            var sqlCommand = new MarkApplicationFeedbacksAsReadSqlCommand();
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
        var sqlCommand = new RequestForApplicationCorrectionSqlCommand();
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
        var sqlCommand = new ResponseToRequestForApplicationCorrectionCommand();
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

