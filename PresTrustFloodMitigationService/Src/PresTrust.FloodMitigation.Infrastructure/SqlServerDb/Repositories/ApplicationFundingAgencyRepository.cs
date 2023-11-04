namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ApplicationFundingAgencyRepository: IApplicationFundingAgencyRepository
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
    public ApplicationFundingAgencyRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
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
    public async Task<List<FloodApplicationFundingAgencyEntity>> GetFundingAgencies(int applicationId)
    {
        List<FloodApplicationFundingAgencyEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFudingAgenciesSqlCommand();
        results = (await conn.QueryAsync<FloodApplicationFundingAgencyEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId })).ToList();

        return results ?? new();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns></returns>
    public async Task<FloodApplicationFundingAgencyEntity> SaveAsync(FloodApplicationFundingAgencyEntity fundingAgency)
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
    private async Task<FloodApplicationFundingAgencyEntity> CreateAsync(FloodApplicationFundingAgencyEntity fundingAgency)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationFundingAgencySqlCommand();
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
    private async Task<FloodApplicationFundingAgencyEntity> UpdateAsync(FloodApplicationFundingAgencyEntity fundingAgency)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationFundingAgencySqlCommand();
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
    public async Task DeleteAsync(FloodApplicationFundingAgencyEntity fundingAgency)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteApplicationFundingAgencySqlCommand();
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
