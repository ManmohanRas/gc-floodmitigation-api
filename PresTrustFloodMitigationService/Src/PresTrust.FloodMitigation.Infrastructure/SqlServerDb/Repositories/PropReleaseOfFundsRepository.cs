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
    public async Task<FloodPropReleaseOfFundsEntity> GetReleaseOfFundsAsync(int applicationId, int Pamspin)
    {
        FloodPropReleaseOfFundsEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetPropReleaseSqlCommand();
        var results = await conn.QueryAsync<FloodPropReleaseOfFundsEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId
                            });

        result = results.FirstOrDefault();

        return result;
    }
    /// <summary>
    /// Save .
    /// </summary>
    /// <param name="floodPropReleaseOfFunds"></param>
    /// <returns></returns>
    public async Task<FloodPropReleaseOfFundsEntity> SaveRofAsync(FloodPropReleaseOfFundsEntity floodPropReleaseOfFunds)
    {
        if (floodPropReleaseOfFunds.Id > 0)
            return await UpdateAsync(floodPropReleaseOfFunds);
        else
            return await SaveAsync(floodPropReleaseOfFunds);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodPropReleaseOfFunds"></param>
    /// <returns></returns>
    private async Task<FloodPropReleaseOfFundsEntity> SaveAsync(FloodPropReleaseOfFundsEntity floodPropReleaseOfFunds)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new CreateTechDetailsSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = floodPropReleaseOfFunds.ApplicationId,
                @p_PamsPin = floodPropReleaseOfFunds.Pamspin,
                @P_ProjectAreaName = floodPropReleaseOfFunds.ProjectAreaName,
                @_Property = floodPropReleaseOfFunds.Property,
                @_ReimburesedHradCost = floodPropReleaseOfFunds.ReimburesedHradCost,
                @_ReimburesedSoftCost = floodPropReleaseOfFunds.ReimburesedSoftCost,
                @_ReimburesedHradSoftCost  = floodPropReleaseOfFunds.ReimburesedHradSoftCost,
                @_CAFNumber = floodPropReleaseOfFunds.CAFNumber,
                @_FinalCost = floodPropReleaseOfFunds.FinalCost,
                @_PaymentMode = floodPropReleaseOfFunds.PaymentMode,
                @_BalanceAmount = floodPropReleaseOfFunds.BalanceAmount,
                @_ReimbureseType = floodPropReleaseOfFunds.ReimbureseType,
                @_ReimbureseAmount = floodPropReleaseOfFunds.ReimbureseAmount,
                @_PaymentType = floodPropReleaseOfFunds.PaymentType,
                @_DateTransfareNeeded = floodPropReleaseOfFunds.DateTransfareNeeded,
                @_PaymentStatus = floodPropReleaseOfFunds.PaymentStatus,
                @_LastUpdatedBy = floodPropReleaseOfFunds.LastUpdatedBy
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
        var sqlCommand = new UpdateTechDetailsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = floodPropReleaseOfFunds.ApplicationId,
                @p_PamsPin = floodPropReleaseOfFunds.Pamspin,
                @P_ProjectAreaName = floodPropReleaseOfFunds.ProjectAreaName,
                @_Property = floodPropReleaseOfFunds.Property,
                @_ReimburesedHradCost = floodPropReleaseOfFunds.ReimburesedHradCost,
                @_ReimburesedSoftCost = floodPropReleaseOfFunds.ReimburesedSoftCost,
                @_ReimburesedHradSoftCost = floodPropReleaseOfFunds.ReimburesedHradSoftCost,
                @_CAFNumber = floodPropReleaseOfFunds.CAFNumber,
                @_FinalCost = floodPropReleaseOfFunds.FinalCost,
                @_PaymentMode = floodPropReleaseOfFunds.PaymentMode,
                @_BalanceAmount = floodPropReleaseOfFunds.BalanceAmount,
                @_ReimbureseType = floodPropReleaseOfFunds.ReimbureseType,
                @_ReimbureseAmount = floodPropReleaseOfFunds.ReimbureseAmount,
                @_PaymentType = floodPropReleaseOfFunds.PaymentType,
                @_DateTransfareNeeded = floodPropReleaseOfFunds.DateTransfareNeeded,
                @_PaymentStatus = floodPropReleaseOfFunds.PaymentStatus,
                @_LastUpdatedBy = floodPropReleaseOfFunds.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });
        return floodPropReleaseOfFunds;
    }
 }   


            

