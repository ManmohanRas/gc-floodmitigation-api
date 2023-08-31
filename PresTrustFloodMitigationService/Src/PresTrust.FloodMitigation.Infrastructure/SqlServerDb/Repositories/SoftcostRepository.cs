namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class SoftcostRepository : ISoftcostRepository
{
    private readonly PresTrustSqlDbContext context;
    private readonly SystemParameterConfiguration systemParamConfig;

    public SoftcostRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<IEnumerable<FloodParcelSoftcostEntity>> GetAllSoftcostLineItemsAsync(int applicationId, string pamsPin)
    {
        IEnumerable<FloodParcelSoftcostEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetSoftcostLineItemsSqlCommand();
        results = await conn.QueryAsync<FloodParcelSoftcostEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new {
                                @p_ApplicationId = applicationId,
                                @p_PamsPin = pamsPin
                            });
        return results;
    }

    public async Task<FloodParcelSoftcostEntity> SaveAsync(FloodParcelSoftcostEntity softcost)
    {
        if (softcost.Id > 0)
            return await UpdateAsync(softcost);
        else
            return await CreateAsync(softcost);
    }
    private async Task<FloodParcelSoftcostEntity> CreateAsync(FloodParcelSoftcostEntity softcost)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateSoftcostLineItemsSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = softcost.ApplicationId,
                @p_PamsPin = softcost.PamsPin,
                @p_SoftcostTypeId = softcost.SoftcostTypeId,
                @p_VendorName = softcost.VendorName,
                @p_InvoiceAmount = softcost.InvoiceAmount,
                @p_PaymentAmount = softcost.PaymentAmount,
                @p_LastUpdatedBy = softcost.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        softcost.Id = id;

        return softcost;
    }
    private async Task<FloodParcelSoftcostEntity> UpdateAsync(FloodParcelSoftcostEntity softcost)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateSoftcostLineItemsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = softcost.Id,
                @p_ApplicationId = softcost.ApplicationId,
                @p_SoftcostTypeId = softcost.SoftcostTypeId,
                @p_PamsPin = softcost.PamsPin,
                @p_VendorName = softcost.VendorName,
                @p_InvoiceAmount = softcost.InvoiceAmount,
                @p_PaymentAmount = softcost.PaymentAmount,
                @p_LastUpdatedBy = softcost.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return softcost;
    }
}

