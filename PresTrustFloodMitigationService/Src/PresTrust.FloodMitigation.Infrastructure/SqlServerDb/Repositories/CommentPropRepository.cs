namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class CommentPropRepository : ICommentPropRepository
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
    public CommentPropRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion

    /// <summary>
    ///  Procedure to fetch all Comments by Application Id.
    /// </summary>
    /// <param name="applicationId"> Id.</param>
    /// <returns> Returns Comments Entity.</returns>
    public async Task<IEnumerable<FloodPropCommentEntity>> GetCommentsAsync(int applicationId, string Pamspin)
    {
        IEnumerable<FloodPropCommentEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetPropCommentsSqlCommend();
        results = await conn.QueryAsync<FloodPropCommentEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });

        return results;
    }

    /// <summary>
    /// Save Comments.
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    public async Task<FloodPropCommentEntity> SaveCommentsAsync(FloodPropCommentEntity comment)
    {
        if (comment.Id > 0)
            return await UpdateAsync(comment);
        else
            return await CreateAsync(comment);
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="dclrOfIntent"></param>
    /// <returns></returns>
    private async Task<FloodPropCommentEntity> CreateAsync(FloodPropCommentEntity comment)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreatePropCommentSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Comment = comment.Comment,
                @p_CommentTypeId = comment.CommentTypeId,
                @p_ApplicationId = comment.ApplicationId,
                @p_Pamspin = comment.Pamspin,
                @p_MarkRead = comment.MarkRead,
                @p_LastUpdatedBy = comment.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        comment.Id = id;

        return comment;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    private async Task<FloodPropCommentEntity> UpdateAsync(FloodPropCommentEntity comment)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdatePropCommentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = comment.Id,
                @p_ApplicationId = comment.ApplicationId,
                @p_Pamspin = comment.Pamspin,
                @p_Comment = comment.Comment,
                @p_CommentTypeId = comment.CommentTypeId,
                @p_LastUpdatedBy = comment.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return comment;
    }

    /// <summary>
    /// Delete Comment
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    public async Task DeleteCommentAsync(FloodPropCommentEntity comment)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeletePropCommentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = comment.Id,
                @p_ApplicationId = comment.ApplicationId,
                @p_Pamspin = comment.Pamspin,
            });
    }
}