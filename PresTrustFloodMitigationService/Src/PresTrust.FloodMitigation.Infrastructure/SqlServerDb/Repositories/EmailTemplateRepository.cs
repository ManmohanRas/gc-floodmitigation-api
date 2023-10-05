namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class EmailTemplateRepository: IEmailTemplateRepository
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
    public EmailTemplateRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion

    /// <summary>
    /// Get Email Template Details
    /// </summary>
    /// <param name="emailTemplateCode"></param>
    /// <returns></returns>
    public async Task<FloodEmailTemplateEntity> GetEmailTemplate(string emailTemplateCode)
    {
        FloodEmailTemplateEntity result = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new GetEmailTemplateSqlCommand();
        var results = await conn.QueryAsync<FloodEmailTemplateEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new { @p_EmailTemplateCode = emailTemplateCode });

        result = results.FirstOrDefault();

        return result;
    }
}
