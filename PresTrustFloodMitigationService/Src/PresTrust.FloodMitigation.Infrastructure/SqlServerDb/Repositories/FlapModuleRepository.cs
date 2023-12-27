namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public class FlapModuleRepository: IFlapModuleRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public FlapModuleRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        ) 
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    /// <summary>
    /// get flap details
    /// </summary>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    public async Task<FloodFlapEntity> GetFlapAsync(int agencyId)
    {
        FloodFlapEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFlapDetailsByAgencyIdSqlCommand();
        var results = await conn.QueryAsync<FloodFlapEntity>(sqlCommand.ToString(),
                                commandType: CommandType.Text,
                                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                                param: new
                                {
                                    @p_AgencyId = agencyId
                                });
        result = results.FirstOrDefault();

        return result ?? new();
    }

    /// <summary>
    /// save flap details
    /// </summary>
    /// <param name="flap"></param>
    /// <returns></returns>
    public async Task<FloodFlapEntity> SaveFlapAsync(FloodFlapEntity flap)
    {
        if (flap.Id > 0)
            return await UpdateFlapAsync(flap);
        else
            return await CreateFlapAsync(flap);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="flap"></param>
    /// <returns></returns>
    private async Task<FloodFlapEntity> CreateFlapAsync(FloodFlapEntity flap)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFlapSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_AgencyId = flap.AgencyId,
                @p_FlapApproved = flap.FlapApproved,
                @p_ApprovedDate = flap.ApprovedDate,
                @p_FlapMailToGrantee = flap.FlapMailToGrantee,
                @p_LastRevisedDate = flap.LastRevisedDate,
                @p_LastUpdatedBy = flap.LastUpdatedBy
            });

        flap.Id = id;

        return flap;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="flap"></param>
    /// <returns></returns>

    private async Task<FloodFlapEntity> UpdateFlapAsync(FloodFlapEntity flap)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFlapSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = flap.Id,
                @p_AgencyId = flap.AgencyId,
                @p_FlapApproved = flap.FlapApproved,
                @p_ApprovedDate = flap.ApprovedDate,
                @p_FlapMailToGrantee = flap.FlapMailToGrantee,
                @p_LastRevisedDate = flap.LastRevisedDate,
                @p_LastUpdatedBy = flap.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return flap;
    }

    /// <summary>
    /// get flap comments
    /// </summary>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    public async Task<List<FloodFlapCommentEntity>> GetFlapCommentsAsync(int agencyId)
    {
        List<FloodFlapCommentEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFlapCommentsByAgencyIdSqlCommand();
        results = (await conn.QueryAsync<FloodFlapCommentEntity>(sqlCommand.ToString(),
                                commandType: CommandType.Text,
                                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                                param: new
                                {
                                    @p_AgencyId = agencyId
                                })).ToList();
        return results;
    }

    /// <summary>
    /// save flap comment
    /// </summary>
    /// <param name="flapComment"></param>
    /// <returns></returns>
    public async Task<FloodFlapCommentEntity> SaveFlapCommentAsync(FloodFlapCommentEntity flapComment)
    {
        if (flapComment.Id > 0)
            return await UpdateFlapCommentAsync(flapComment);
        else
            return await CreateFlapCommentAsync(flapComment);
    }

    private async Task<FloodFlapCommentEntity> CreateFlapCommentAsync(FloodFlapCommentEntity flapComment)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFlapCommentSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_AgencyId = flapComment.AgencyId,
                @p_Comment = flapComment.Comment,
                @p_LastUpdatedBy = flapComment.LastUpdatedBy
            });

        flapComment.Id = id;

        return flapComment;
    }

    private async Task<FloodFlapCommentEntity> UpdateFlapCommentAsync(FloodFlapCommentEntity flapComment)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFlapCommentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = flapComment.Id,
                @p_AgencyId = flapComment.AgencyId,
                @p_Comment = flapComment.Comment,
                @p_LastUpdatedBy = flapComment.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return flapComment;
    }

    public async Task<List<FloodFlapDocumentEntity>> GetFlapDocumentsAsync(int agencyId)
    {
        List<FloodFlapDocumentEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFlapDocumentsSqlCommand();
        results = (await conn.QueryAsync<FloodFlapDocumentEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_AgencyId = agencyId })).ToList();

        return results ?? new();
    }

    public async Task<FloodFlapDocumentEntity> SaveFlapDocumentAsync(FloodFlapDocumentEntity doc)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new SaveFlapDocumentSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_FileName = doc.FileName,
                @p_Title = doc.Title,
                @p_DocumentTypeId = (int)doc.DocumentType,
                @p_AgencyId = doc.AgencyId,
                @p_LastUpdatedBy = doc.LastUpdatedBy,
            });

        doc.Id = id;

        return doc;
    }

    public async Task DeleteFlapDocumentAsync(int id)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteFlapDocumentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new { @p_Id = id });
    }

    /// <summary>
    /// Delete flap comment
    /// </summary>
    /// <param name="flapComment"></param>
    /// <returns></returns>
    public async Task DeleteFlapCommentAsync(FloodFlapCommentEntity flapComment)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteFlapCommentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = flapComment.Id
            });
    }

    public async Task<FloodFlapTargetAreaEntity> GetFlapTargetAreaByIdAsync(int Id)
    {
        FloodFlapTargetAreaEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetTargetAreaDetailsByIdSqlCommand();
        var results = await conn.QueryAsync<FloodFlapTargetAreaEntity>(sqlCommand.ToString(),
                                commandType: CommandType.Text,
                                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                                param: new
                                {
                                    @p_Id = Id
                                });

        result = results.FirstOrDefault();

        return result ?? new();
    }

    /// <summary>
    /// Get flap target areas 
    /// </summary>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FloodFlapTargetAreaEntity>> GetFlapTargetAreasAsync(int agencyId)
    {
        IEnumerable<FloodFlapTargetAreaEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFlapTargetAreasByAgencyIdSqlCommand();
        results = (await conn.QueryAsync<FloodFlapTargetAreaEntity>(sqlCommand.ToString(),
                                commandType: CommandType.Text,
                                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                                param: new
                                {
                                    @p_AgencyId = agencyId
                                })).ToList();
        return results;
    }

    /// <summary>
    /// Save target area
    /// </summary>
    /// <param name="targetArea"></param>
    /// <returns></returns>
    public async Task<FloodFlapTargetAreaEntity> SaveFlapTargetAreaAsync(FloodFlapTargetAreaEntity targetArea)
    {
        if (targetArea.Id > 0)
            return await UpdateFlapTargetAreaAsync(targetArea);
        else
            return await CreateFlapTargetAreaAsync(targetArea);
    }

    private async Task<FloodFlapTargetAreaEntity> CreateFlapTargetAreaAsync(FloodFlapTargetAreaEntity targetArea)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFlapTargetAreaSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_AgencyId = targetArea.AgencyId,
                @p_TargetArea = targetArea.TargetArea,
                @p_CreatedDate = targetArea.CreatedDate,
                @p_LastUpdatedBy = targetArea.LastUpdatedBy
            });

        targetArea.Id = id;

        return targetArea;
    }

    private async Task<FloodFlapTargetAreaEntity> UpdateFlapTargetAreaAsync(FloodFlapTargetAreaEntity targetArea)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFlapTargetAreaSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = targetArea.Id,
                @p_AgencyId = targetArea.AgencyId,
                @p_TargetArea = targetArea.TargetArea,
                @p_CreatedDate = targetArea.CreatedDate,
                @p_LastUpdatedBy = targetArea.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return targetArea;
    }
}
