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

    public async Task<IEnumerable<FloodCommentsEntity>> GetAllCommentsAsync(int applicationId)
    {
        IEnumerable<FloodCommentsEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetAllCommentsSqlCommand();
        results = await conn.QueryAsync<FloodCommentsEntity>(sqlCommand.ToString(),
            commandType:CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });
        return results;
    }

    public async Task<FloodCommentsEntity> SaveAsync(FloodCommentsEntity comment)
    {
        if (comment.Id > 0)
            return await UpdateAsync(comment);
        else
            return await CreateAsync(comment);
    }

    private async Task<FloodCommentsEntity> UpdateAsync(FloodCommentsEntity comment)
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
    private async Task<FloodCommentsEntity> CreateAsync(FloodCommentsEntity comment)
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
                @p_MarkRead = comment.MarkRead,
                @p_LastUpdatedBy = comment.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        comment.Id = id;

        return comment;
    }

    public async Task DeleteCommentAsync(FloodCommentsEntity comment)
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
