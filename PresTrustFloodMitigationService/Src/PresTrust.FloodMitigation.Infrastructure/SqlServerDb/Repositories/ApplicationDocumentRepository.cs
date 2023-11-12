namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ApplicationDocumentRepository: IApplicationDocumentRepository
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
    public ApplicationDocumentRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion
    /// <summary>
    ///  Procedure to fetch Document by Application Id.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Document collection.</returns>
    public async Task<List<FloodApplicationDocumentEntity>> GetApplicationDocumentsAsync(int applicationId, int sectionId)
    {
        List<FloodApplicationDocumentEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationDocumentsSqlCommand();
        results = (await conn.QueryAsync<FloodApplicationDocumentEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId, @p_SectionId = sectionId })).ToList();

        return results ?? new();
    }

    /// Procedure to save uploaded document 
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public async Task<FloodApplicationDocumentEntity> SaveApplicationDocumentDetailsAsync(FloodApplicationDocumentEntity doc)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new SaveApplicationDocumentSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_FileName = doc.FileName,
                @p_Title = doc.Title,
                @p_Description = doc.Description,
                @p_HardCopy = doc.HardCopy,
                @p_Approved = doc.Approved,
                @p_ReviewComment = doc.ReviewComment,
                @p_UseInReport = doc.UseInReport,
                @p_DocumentTypeId = (int)doc.DocumentType,
                @p_ApplicationId = doc.ApplicationId,
                @p_LastUpdatedBy = doc.LastUpdatedBy,
            });

        doc.Id = id;

        return doc;
    }

    /// <summary>
    /// Procedure to delete document 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteApplicationDocumentAsync(int id)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteApplicationDocumentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new { @p_Id = id });
    }

    public async Task<List<FloodApplicationDocumentEntity>> GetApplicationDocumentChecklistAsync(int applicationId)
    {
        List<FloodApplicationDocumentEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationDocumentChecklistSqlCommand();
        results = (await conn.QueryAsync<FloodApplicationDocumentEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId })).ToList();

        return results ?? new();
    }

    public async Task<FloodApplicationDocumentEntity> SaveApplicationDocumentChecklistAsync(FloodApplicationDocumentEntity doc)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationDocumentChecklistSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = doc.Id,
                @p_Title = doc.Title,
                @p_Description = doc.Description,
                @p_HardCopy = doc.HardCopy,
                @p_Approved = doc.Approved,
                @p_ReviewComment = doc.ReviewComment,
                @p_UseInReport = doc.UseInReport,
                @p_LastUpdatedBy = doc.LastUpdatedBy,
                @p_OtherFundingSourceId = doc.OtherFundingSourceId
            });

        return doc;
    }
}
