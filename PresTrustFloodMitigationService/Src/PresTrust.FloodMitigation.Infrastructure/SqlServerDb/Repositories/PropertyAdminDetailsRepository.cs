namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public class PropertyAdminDetailsRepository : IPropertyAdminDetailsRepository
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
    public PropertyAdminDetailsRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion
    public async Task<FloodPropertyAdminDetailsEntity> GetAsync(int applicationId, string Pamspin)
    {
        FloodPropertyAdminDetailsEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetPropertyAdminDetailsSqlCommand();
        var results = await conn.QueryAsync<FloodPropertyAdminDetailsEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId,
                                @p_Pamspin = Pamspin,
                            });

        result = results.FirstOrDefault();

        return result;
    }
    /// <summary>
    /// Save .
    /// </summary>
    /// <param name="floodPropDetails"></param>
    /// <returns></returns>
    public async Task<FloodPropertyAdminDetailsEntity> SaveAsync(FloodPropertyAdminDetailsEntity floodPropDetails)
    {
        if (floodPropDetails.Id > 0)
            return await UpdateAsync(floodPropDetails);
        else
            return await CreateAsync(floodPropDetails);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodAppDetails"></param>
    /// <returns></returns>
    private async Task<FloodPropertyAdminDetailsEntity> CreateAsync(FloodPropertyAdminDetailsEntity floodPropDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new CreatePropertyAdminDetailsSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_ApplicationId = floodPropDetails.ApplicationId,
                @P_PamsPin = floodPropDetails.PamsPin,

                //@P_HardCostPaymentTypeId = floodPropReleaseOfFunds.HardCostPaymentTypeId,
                //@P_HardCostPaymentDate = floodPropReleaseOfFunds.HardCostPaymentDate,
                //@P_HardCostPaymentStatusId = floodPropReleaseOfFunds.HardCostPaymentStatusId,
                //@P_SoftCostPaymentTypeId = floodPropReleaseOfFunds.SoftCostPaymentTypeId,
                //@P_SoftCostPaymentDate = floodPropReleaseOfFunds.SoftCostPaymentDate,
                //@P_SoftCostPaymentStatusId = floodPropReleaseOfFunds.SoftCostPaymentStatusId,
                  @P_LastUpdatedBy = floodPropDetails.LastUpdatedBy
            });
        floodPropDetails.Id = id;

        return floodPropDetails;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodPropDetails"></param>
    /// <returns></returns>
    private async Task<FloodPropertyAdminDetailsEntity> UpdateAsync(FloodPropertyAdminDetailsEntity floodPropDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdatePropertyAdminDetailsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_Id = floodPropDetails.Id,
                @P_ApplicationId = floodPropDetails.ApplicationId,
                @P_PamsPin = floodPropDetails.PamsPin,

                //@P_PamsPin = floodPropReleaseOfFunds.PamsPin,
                //@P_HardCostPaymentTypeId = floodPropReleaseOfFunds.HardCostPaymentTypeId,
                //@P_HardCostPaymentDate = floodPropReleaseOfFunds.HardCostPaymentDate,
                //@P_HardCostPaymentStatusId = floodPropReleaseOfFunds.HardCostPaymentStatusId,
                //@P_SoftCostPaymentTypeId = floodPropReleaseOfFunds.SoftCostPaymentTypeId,
                //@P_SoftCostPaymentDate = floodPropReleaseOfFunds.SoftCostPaymentDate,
                //@P_SoftCostPaymentStatusId = floodPropReleaseOfFunds.SoftCostPaymentStatusId,
                @P_LastUpdatedBy = floodPropDetails.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });
        return floodPropDetails;
    }
}
