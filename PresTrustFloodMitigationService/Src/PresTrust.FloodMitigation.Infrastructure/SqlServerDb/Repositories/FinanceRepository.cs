namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class FinanceRepository: IFinanceRepository
{
    #region " Members ... "

    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    #endregion

    #region " ctor ... "
    public FinanceRepository(
       PresTrustSqlDbContext context,
       IOptions<SystemParameterConfiguration> systemParamConfigOptions
       )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }
    #endregion

    /// <summary>
    ///  Procedure to fetch finance details by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns finance details.</returns>
    public async Task<FloodApplicationFinanceEntity> GetFinanceAsync(int applicationId)
    {
        FloodApplicationFinanceEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFinanceSqlCommand();
        var results = await conn.QueryAsync<FloodApplicationFinanceEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });
        result = results.FirstOrDefault();
        return result ?? new();
    }

    /// <summary>
    /// Save Finance.
    /// </summary>
    /// <param name="finance"></param>
    /// <returns></returns>
    public async Task<FloodApplicationFinanceEntity> SaveAsync(FloodApplicationFinanceEntity finance)
    {
        if (finance.Id > 0)
            return await UpdateAsync(finance);
        else
            return await CreateAsync(finance);

    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="finance"></param>
    /// <returns></returns>
    private async Task<FloodApplicationFinanceEntity> CreateAsync(FloodApplicationFinanceEntity finance)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFinanceSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = finance.ApplicationId,
                @p_MatchPercent = finance.MatchPercent,
                @p_LastUpdatedBy = finance.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        finance.Id = id;

        return finance;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="finance"></param>
    /// <returns></returns>
    private async Task<FloodApplicationFinanceEntity> UpdateAsync(FloodApplicationFinanceEntity finance)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFinanceSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = finance.Id,
                @p_ApplicationId = finance.ApplicationId,
                @p_MatchPercent = finance.MatchPercent,
                @p_LastUpdatedBy = finance.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return finance;
    }
}
