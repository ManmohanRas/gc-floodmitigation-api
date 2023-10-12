namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class PropertyDocumentRepository: IPropertyDocumentRepository
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
    public PropertyDocumentRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion
    public async Task<IEnumerable<FloodPropertyDocumentEntity>> GetPropertyDocumentsAsync(int applicationId, int sectionId, string pamsPin)
    {
        IEnumerable<FloodPropertyDocumentEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetPropertyDocumentsSqlCommand();
        results = await conn.QueryAsync<FloodPropertyDocumentEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId, @p_SectionId = sectionId, @p_pamsPin = pamsPin});

        return results;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parcelDoc"></param>
    /// <returns></returns>
    public async Task<FloodPropertyDocumentEntity> SavePropertyDocumentDetailsAsync(FloodPropertyDocumentEntity parcelDoc)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new SavePropertyDocumentSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_FileName = parcelDoc.FileName,
                @p_Title = parcelDoc.Title,
                @p_Description = parcelDoc.Description,
                @p_HardCopy = parcelDoc.HardCopy,
                @p_Approved = parcelDoc.Approved,
                @p_ReviewComment = parcelDoc.ReviewComment,
                @p_UseInReport = parcelDoc.UseInReport,
                @p_DocumentTypeId = (int)parcelDoc.DocumentType,
                @p_Pamspin = parcelDoc.Pamspin,
                @p_ApplicationId = parcelDoc.ApplicationId,
                @p_LastUpdatedBy = parcelDoc.LastUpdatedBy,
            });

        parcelDoc.Id = id;

        return parcelDoc;
    }

    public async Task DeletePropertyDocumentAsync(int id)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeletePropertyDocumentSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new { @p_Id = id });
    }
}
