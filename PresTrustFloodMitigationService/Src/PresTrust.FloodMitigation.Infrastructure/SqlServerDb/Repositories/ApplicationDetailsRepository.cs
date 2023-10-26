namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public class ApplicationDetailsRepository : IApplicationDetailsRepository
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
    public ApplicationDetailsRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion
    public async Task<FloodApplicationDetailsEntity> GetAsync(int applicationId)
    {
        FloodApplicationDetailsEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationDetailsSqlCommand();
        var results = await conn.QueryAsync<FloodApplicationDetailsEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId,
                            });

        result = results.FirstOrDefault();

        return result;
    }
    /// <summary>
    /// Save .
    /// </summary>
    /// <param name="floodAppDetails"></param>
    /// <returns></returns>
    public async Task<FloodApplicationDetailsEntity> SaveAsync(FloodApplicationDetailsEntity floodAppDetails)
    {
        if (floodAppDetails.Id > 0)
            return await UpdateAsync(floodAppDetails);
        else
            return await CreateAsync(floodAppDetails);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodAppDetails"></param>
    /// <returns></returns>
    private async Task<FloodApplicationDetailsEntity> CreateAsync(FloodApplicationDetailsEntity floodAppDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationDetailsSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_ApplicationId = floodAppDetails.ApplicationId,

                //@P_PamsPin = floodPropReleaseOfFunds.PamsPin,
                //@P_HardCostPaymentTypeId = floodPropReleaseOfFunds.HardCostPaymentTypeId,
                //@P_HardCostPaymentDate = floodPropReleaseOfFunds.HardCostPaymentDate,
                //@P_HardCostPaymentStatusId = floodPropReleaseOfFunds.HardCostPaymentStatusId,
                //@P_SoftCostPaymentTypeId = floodPropReleaseOfFunds.SoftCostPaymentTypeId,
                //@P_SoftCostPaymentDate = floodPropReleaseOfFunds.SoftCostPaymentDate,
                //@P_SoftCostPaymentStatusId = floodPropReleaseOfFunds.SoftCostPaymentStatusId,
                //@P_LastUpdatedBy = floodPropReleaseOfFunds.LastUpdatedBy
            });
        floodAppDetails.Id = id;

        return floodAppDetails;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodAppDetails"></param>
    /// <returns></returns>
    private async Task<FloodApplicationDetailsEntity> UpdateAsync(FloodApplicationDetailsEntity floodAppDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationDetailsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_Id = floodAppDetails.Id,
                @P_ApplicationId = floodAppDetails.ApplicationId,

                //@P_PamsPin = floodPropReleaseOfFunds.PamsPin,
                //@P_HardCostPaymentTypeId = floodPropReleaseOfFunds.HardCostPaymentTypeId,
                //@P_HardCostPaymentDate = floodPropReleaseOfFunds.HardCostPaymentDate,
                //@P_HardCostPaymentStatusId = floodPropReleaseOfFunds.HardCostPaymentStatusId,
                //@P_SoftCostPaymentTypeId = floodPropReleaseOfFunds.SoftCostPaymentTypeId,
                //@P_SoftCostPaymentDate = floodPropReleaseOfFunds.SoftCostPaymentDate,
                //@P_SoftCostPaymentStatusId = floodPropReleaseOfFunds.SoftCostPaymentStatusId,
                //@P_LastUpdatedBy = floodPropReleaseOfFunds.LastUpdatedBy,
                //@p_LastUpdatedOn = DateTime.Now
            });
        return floodAppDetails;
    }

}
