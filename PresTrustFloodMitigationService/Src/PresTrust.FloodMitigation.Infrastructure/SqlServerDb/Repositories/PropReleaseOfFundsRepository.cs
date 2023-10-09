using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommandstails;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public class PropReleaseOfFundsRepository : IPropReleaseOfFundsRepository
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
    public PropReleaseOfFundsRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion
    public async Task<FloodPropReleaseOfFundsEntity> GetReleaseOfFundsAsync(int applicationId, string Pamspin)
    {
        FloodPropReleaseOfFundsEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetPropReleaseSqlCommand();
        var results = await conn.QueryAsync<FloodPropReleaseOfFundsEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId,
                                @p_PamsPin = Pamspin
                            });

        result = results.FirstOrDefault();

        return result;
    }
    /// <summary>
    /// Save .
    /// </summary>
    /// <param name="floodPropReleaseOfFunds"></param>
    /// <returns></returns>
    public async Task<FloodPropReleaseOfFundsEntity> SaveAsync(FloodPropReleaseOfFundsEntity floodPropReleaseOfFunds)
    {
        if (floodPropReleaseOfFunds.Id > 0)
            return await UpdateAsync(floodPropReleaseOfFunds);
        else
            return await CreateAsync(floodPropReleaseOfFunds);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodPropReleaseOfFunds"></param>
    /// <returns></returns>
    private async Task<FloodPropReleaseOfFundsEntity> CreateAsync(FloodPropReleaseOfFundsEntity floodPropReleaseOfFunds)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new CreatePropReleaseSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_ApplicationId = floodPropReleaseOfFunds.ApplicationId,
                @P_PamsPin = floodPropReleaseOfFunds.PamsPin,
                @P_HardCostPaymentTypeId = floodPropReleaseOfFunds.HardCostPaymentTypeId,
                @P_HardCostPaymentDate = floodPropReleaseOfFunds.HardCostPaymentDate,
                @P_HardCostPaymentStatusId = floodPropReleaseOfFunds.HardCostPaymentStatusId,
                @P_SoftCostPaymentTypeId = floodPropReleaseOfFunds.SoftCostPaymentTypeId,
                @P_SoftCostPaymentDate = floodPropReleaseOfFunds.SoftCostPaymentDate,
                @P_SoftCostPaymentStatusId = floodPropReleaseOfFunds.SoftCostPaymentStatusId,
                @P_LastUpdatedBy = floodPropReleaseOfFunds.LastUpdatedBy
            });
        floodPropReleaseOfFunds.Id = id;

        return floodPropReleaseOfFunds;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodPropReleaseOfFunds"></param>
    /// <returns></returns>
    private async Task<FloodPropReleaseOfFundsEntity> UpdateAsync(FloodPropReleaseOfFundsEntity floodPropReleaseOfFunds)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdatePropReleaseOfFundsCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_ApplicationId = floodPropReleaseOfFunds.ApplicationId,
                @P_PamsPin = floodPropReleaseOfFunds.PamsPin,
                @P_HardCostPaymentTypeId = floodPropReleaseOfFunds.HardCostPaymentTypeId,
                @P_HardCostPaymentDate = floodPropReleaseOfFunds.HardCostPaymentDate,
                @P_HardCostPaymentStatusId = floodPropReleaseOfFunds.HardCostPaymentStatusId,
                @P_SoftCostPaymentTypeId = floodPropReleaseOfFunds.SoftCostPaymentTypeId,
                @P_SoftCostPaymentDate = floodPropReleaseOfFunds.SoftCostPaymentDate,
                @P_SoftCostPaymentStatusId = floodPropReleaseOfFunds.SoftCostPaymentStatusId,
                @P_LastUpdatedBy = floodPropReleaseOfFunds.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });
        return floodPropReleaseOfFunds;
    }
 }   


            

