namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class FundingAgencyRepository: IFundingAgencyRepository
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
    public FundingAgencyRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FloodFundingAgencyEntity>> GetFundingAgencies(int applicationId)
    {
        IEnumerable<FloodFundingAgencyEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFudingAgenciesSqlCommand();
        results = await conn.QueryAsync<FloodFundingAgencyEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });

        return results;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns></returns>
    public async Task<FloodFundingAgencyEntity> SaveAsync(FloodFundingAgencyEntity fundingAgency)
    {
        if (fundingAgency.Id > 0)
            return await UpdateAsync(fundingAgency);
        else
            return await CreateAsync(fundingAgency);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns></returns>
    private async Task<FloodFundingAgencyEntity> CreateAsync(FloodFundingAgencyEntity fundingAgency)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFundingAgencySqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = fundingAgency.ApplicationId,
                @p_FundingAgencyName = fundingAgency.FundingAgencyName,
                @p_CurrentStatus = fundingAgency.CurrentStatus,
                @p_DateOfApproval = fundingAgency.DateOfApproval
            });

        fundingAgency.Id = id;

        return fundingAgency;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns></returns>
    private async Task<FloodFundingAgencyEntity> UpdateAsync(FloodFundingAgencyEntity fundingAgency)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFundingAgencySqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = fundingAgency.Id,
                @p_ApplicationId = fundingAgency.ApplicationId,
                @p_FundingAgencyName = fundingAgency.FundingAgencyName,
                @p_CurrentStatus = fundingAgency.CurrentStatus,
                @p_DateOfApproval = fundingAgency.DateOfApproval
            });

        return fundingAgency;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns></returns>
    public async Task DeleteAsync(FloodFundingAgencyEntity fundingAgency)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteFundingAgencySqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = fundingAgency.Id,
                @p_ApplicationId = fundingAgency.ApplicationId,
            });
    }

}
