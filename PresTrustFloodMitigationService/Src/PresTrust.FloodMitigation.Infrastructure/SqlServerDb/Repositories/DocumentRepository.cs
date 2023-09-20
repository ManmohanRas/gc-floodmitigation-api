using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Documents;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class DocumentRepository: IDocumentRepository
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
    public DocumentRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
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
    public async Task<IEnumerable<FloodDocumentEntity>> GetDocumentsAsync(int applicationId, int sectionId)
    {
        IEnumerable<FloodDocumentEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetDocumentsSqlCommand();
        results = await conn.QueryAsync<FloodDocumentEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId, @p_SectionId = sectionId });

        return results;
    }

    /// Procedure to save uploaded document 
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public async Task<FloodDocumentEntity> SaveDocumentDetailsAsync(FloodDocumentEntity doc)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new SaveDocumentSqlCommand();
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
    public async Task DeleteDocumentAsync(int id)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteDocumentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new { @p_Id = id });
    }

    public async Task<IEnumerable<FloodDocumentEntity>> GetDocumentCheckListAsync(int applicationId, bool hasCOEDocument)
    {
        IEnumerable<FloodDocumentEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetDocumentCheckListSqlCommand(hasCOEDocument);
        results = await conn.QueryAsync<FloodDocumentEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });

        return results;
    }

    public async Task<FloodDocumentEntity> UpdateDocumentCheckListItemsAsync(FloodDocumentEntity doc)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateDocumentCheckListItemsSqlCommand();
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
            });

        return doc;
    }
}
