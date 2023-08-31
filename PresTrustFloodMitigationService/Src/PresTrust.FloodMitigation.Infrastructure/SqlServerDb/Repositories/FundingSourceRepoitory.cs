namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class FundingSourceRepoitory: IFundingSourceRepoitory
{
    #region " Members ... "

    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    #endregion

    #region " ctor ... "
    public FundingSourceRepoitory(
       PresTrustSqlDbContext context,
       IOptions<SystemParameterConfiguration> systemParamConfigOptions
       )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }
    #endregion

    /// <summary>
    ///  Procedure to fetch all funding source items by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns funding source items.</returns>
    public async Task<IEnumerable<FloodFundingSourceEntity>> GetFundingSourcesAsync(int applicationId)
    {
        IEnumerable<FloodFundingSourceEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFundingSourcesByApplicationIdSqlCommad();
        results = await conn.QueryAsync<FloodFundingSourceEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });

        return results;
    }

    /// <summary>
    /// Save Funding source.
    /// </summary>
    /// <param name="fundingSource"></param>
    /// <returns></returns>
    public async Task<FloodFundingSourceEntity> SaveAsync(FloodFundingSourceEntity fundingSource)
    {
        if (fundingSource.Id > 0)
            return await UpdateAsync(fundingSource);
        else
            return await CreateAsync(fundingSource);
        
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="fundingSource"></param>
    /// <returns></returns>
    private async Task<FloodFundingSourceEntity> CreateAsync(FloodFundingSourceEntity fundingSource)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFundingSourceSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = fundingSource.ApplicationId,
                @p_FundingSourceTypeId = (int)fundingSource.FundingSourceType,
                @p_Title = fundingSource.Title,
                @p_Amount = fundingSource.Amount,
                @p_AwardDate = fundingSource.AwardDate,
                @p_LastUpdatedBy = fundingSource.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        fundingSource.Id = id;

        return fundingSource;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="fundingSource"></param>
    /// <returns></returns>
    private async Task<FloodFundingSourceEntity> UpdateAsync(FloodFundingSourceEntity fundingSource)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFundingSourceSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = fundingSource.Id,
                @p_ApplicationId = fundingSource.ApplicationId,
                @p_Title = fundingSource.Title,
                @p_Amount = fundingSource.Amount,
                @p_AwardDate = fundingSource.AwardDate,
                @p_LastUpdatedBy = fundingSource.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return fundingSource;
    }


    /// <summary>
    /// Delete Funding Source.
    /// </summary>
    /// <returns></returns>
    public async Task DeleteAsync(FloodFundingSourceEntity fund)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteFundingSourceSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = fund.Id,
                @p_ApplicationId = fund.ApplicationId
            });
    }
}
