namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public CommentRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<IEnumerable<FloodCommentEntity>> GetAllCommentsAsync(int applicationId)
    {
        IEnumerable<FloodCommentEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetAllCommentsSqlCommand();
        results = await conn.QueryAsync<FloodCommentEntity>(sqlCommand.ToString(),
            commandType:CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });
        return results;
    }

    public async Task<FloodCommentEntity> SaveAsync(FloodCommentEntity comment)
    {
        if (comment.Id > 0)
            return await UpdateAsync(comment);
        else
            return await CreateAsync(comment);
    }

    private async Task<FloodCommentEntity> UpdateAsync(FloodCommentEntity comment)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateCommentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = comment.Id,
                @p_ApplicationId = comment.ApplicationId,
                @p_Comment = comment.Comment,
                @p_CommentTypeId = comment.CommentTypeId,
                @p_LastUpdatedBy = comment.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return comment;
    }
    private async Task<FloodCommentEntity> CreateAsync(FloodCommentEntity comment)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateCommentSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Comment = comment.Comment,
                @p_CommentTypeId = comment.CommentTypeId,
                @p_ApplicationId = comment.ApplicationId,
                @p_LastUpdatedBy = comment.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        comment.Id = id;

        return comment;
    }

    public async Task DeleteCommentAsync(FloodCommentEntity comment)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteCommentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = comment.Id,
                @p_ApplicationId = comment.ApplicationId
            });
    }
}
