using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class FeedbackPropRepository : IFeedbackPropRepository
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
    public FeedbackPropRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
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
    public async Task<List<FloodPropertyFeedbackEntity>> GetPropertyFeedbackAsync(int applicationId, string pamsPin, string correctionStatus)
    {
        List<FloodPropertyFeedbackEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetPropertyFeedbackSqlCommand();
        results = (await conn.QueryAsync<FloodPropertyFeedbackEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new {
                                @p_ApplicationId = applicationId,
                                @p_PamsPin = pamsPin,
                                @p_CorrectionStatus = correctionStatus
                            })).ToList();

        return results ?? new();
    }

    /// <summary>
    /// Save Feedback.
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    public async Task<FloodPropertyFeedbackEntity> SavePropertyFeedbackAsync(FloodPropertyFeedbackEntity feedback)
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
    private async Task<FloodPropertyFeedbackEntity> CreateAsync(FloodPropertyFeedbackEntity feedback)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreatePropertyFeedbackSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = feedback.ApplicationId,
                @P_PamsPin = feedback.PamsPin,
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
    private async Task<FloodPropertyFeedbackEntity> UpdateAsync(FloodPropertyFeedbackEntity feedback)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdatePropertyFeedbackSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = feedback.Id,
                @p_ApplicationId = feedback.ApplicationId,
                @P_PamsPin = feedback.PamsPin,
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
    public async Task DeletePropertyFeedbackAsync(FloodPropertyFeedbackEntity feedback)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeletePropertyFeedbackSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = feedback.Id,
                @P_PamsPin = feedback.PamsPin,
                @p_ApplicationId = feedback.ApplicationId,

            });
    }
    public async Task MarkPropertyFeedbackAsReadAsync(List<int> feedbackIds)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new MarkPropertyFeedbackAsReadSqlCommand();
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
    public async Task RequestForPropertyCorrectionAsync(int applicationId, string pamsPin)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new RequestForPropertyCorrectionCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = applicationId,
                @p_PamsPin = pamsPin,
                @p_CorrectionStatus = ApplicationCorrectionStatusEnum.REQUEST_SENT.ToString(),
            });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    public async Task ResponseToRequestForPropertyCorrectionAsync(int applicationId, string pamsPin, int sectionId)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new ResponseToRequestForPropertyCorrectionCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = applicationId,
                @p_PamsPin = pamsPin,
                @p_SectionId = sectionId,
                @p_CorrectionStatus = ApplicationCorrectionStatusEnum.RESPONSE_RECEIVED.ToString(),
            });
    }
}
