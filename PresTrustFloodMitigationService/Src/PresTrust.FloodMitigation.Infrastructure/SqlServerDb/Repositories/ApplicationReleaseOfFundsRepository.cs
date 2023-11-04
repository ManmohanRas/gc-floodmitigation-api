namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public class ApplicationReleaseOfFundsRepository: IApplicationReleaseOfFundsRepository
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
    public ApplicationReleaseOfFundsRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion

    /// <summary>
    ///  Procedure to fetch Release of funds by Id.
    /// </summary>
    /// <param name="applicationId"> Id.</param>
    /// <returns> Returns FloodApplicationReleaseOfFundsEntity.</returns>
    public async Task<FloodApplicationReleaseOfFundsEntity> GetReleaseOfFundsAsync(int applicationId)
    {
        FloodApplicationReleaseOfFundsEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationReleaseOfFundsSqlCommand();
        var results = await conn.QueryAsync<FloodApplicationReleaseOfFundsEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId
                            });

        result = results.FirstOrDefault();

        return result ?? new();
    }

    /// <summary>
    ///  Procedure to fetch Application payments.
    /// </summary>
    /// <param name="applicationId"> Id.</param>
    /// <returns> Returns FloodPropReleaseOfFundsEntity.</returns>
    public async Task<List<FloodPropReleaseOfFundsEntity>> GetApplicationPaymentsAsync(int applicationId)
    {
        List<FloodPropReleaseOfFundsEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationPaymentsSqlCommand();
        results = (await conn.QueryAsync<FloodPropReleaseOfFundsEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId,
                            })).ToList();


        return results ?? new();
    }

    /// <summary>
    /// Save Release of funds.
    /// </summary>
    /// <param name="releaseOfFunds"></param>
    /// <returns></returns>
    public async Task<FloodApplicationReleaseOfFundsEntity> SaveAsync(FloodApplicationReleaseOfFundsEntity releaseOfFunds)
    {
        if (releaseOfFunds.Id > 0)
            return await UpdateAsync(releaseOfFunds);
        else
            return await CreateAsync(releaseOfFunds);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="releaseOfFunds"></param>
    /// <returns></returns>
    private async Task<FloodApplicationReleaseOfFundsEntity> CreateAsync(FloodApplicationReleaseOfFundsEntity releaseOfFunds)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationReleaseOfFundsSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = releaseOfFunds.ApplicationId,
                @p_CAFNumber = releaseOfFunds.CAFNumber,
                @p_CAFClosed = releaseOfFunds.CAFClosed,
                @p_LastUpdatedBy = releaseOfFunds.LastUpdatedBy
            });

        releaseOfFunds.Id = id;

        return releaseOfFunds;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="releaseOfFunds"></param>
    /// <returns></returns>
    private async Task<FloodApplicationReleaseOfFundsEntity> UpdateAsync(FloodApplicationReleaseOfFundsEntity releaseOfFunds)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationReleaseOfFundsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = releaseOfFunds.Id,
                @p_ApplicationId = releaseOfFunds.ApplicationId,
                @p_CAFNumber = releaseOfFunds.CAFNumber,
                @p_CAFClosed = releaseOfFunds.CAFClosed,
                @p_LastUpdatedBy = releaseOfFunds.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return releaseOfFunds;
    }
}
