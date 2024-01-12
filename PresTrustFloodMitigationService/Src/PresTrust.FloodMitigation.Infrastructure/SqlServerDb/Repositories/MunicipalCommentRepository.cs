namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class MunicipalCommentRepository: IMunicipalCommentRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public MunicipalCommentRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<List<FloodMunicipalCommentEntity>> GetAllCommentsAsync(int agencyId)
    {
        List<FloodMunicipalCommentEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetAllMunicipalCommentsSqlCommand();
        results = (await conn.QueryAsync<FloodMunicipalCommentEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_AgencyId = agencyId })).ToList();
        return results ?? new();
    }

    public async Task<FloodMunicipalCommentEntity> SaveAsync(FloodMunicipalCommentEntity comment)
    {
        if (comment.Id > 0)
            return await UpdateAsync(comment);
        else
            return await CreateAsync(comment);
    }

    private async Task<FloodMunicipalCommentEntity> UpdateAsync(FloodMunicipalCommentEntity comment)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationCommentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = comment.Id,
                @p_AgencyId = comment.AgencyId,
                @p_Comment = comment.Comment,
                @p_LastUpdatedBy = comment.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return comment;
    }
    private async Task<FloodMunicipalCommentEntity> CreateAsync(FloodMunicipalCommentEntity comment)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateMunicipalCommentSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Comment = comment.Comment,
                @p_AgencyId = comment.AgencyId,
                @p_LastUpdatedBy = comment.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        comment.Id = id;

        return comment;
    }

    public async Task DeleteCommentAsync(FloodMunicipalCommentEntity comment)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteApplicationCommentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = comment.Id,
                @p_AgencyId = comment.AgencyId
            });
    }
}
