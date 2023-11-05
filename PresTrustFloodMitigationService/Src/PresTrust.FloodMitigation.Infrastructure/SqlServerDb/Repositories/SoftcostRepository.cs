namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class SoftCostRepository : ISoftCostRepository
{
    private readonly PresTrustSqlDbContext context;
    private readonly SystemParameterConfiguration systemParamConfig;

    public SoftCostRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<List<FloodParcelSoftCostEntity>> GetAllSoftCostLineItemsAsync(int applicationId, string pamsPin)
    {
        List<FloodParcelSoftCostEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetSoftCostLineItemsSqlCommand();
        results = (await conn.QueryAsync<FloodParcelSoftCostEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new {
                                @p_ApplicationId = applicationId,
                                @p_PamsPin = pamsPin
                            })).ToList();
        return results ?? new();
    }

    public async Task<FloodParcelSoftCostEntity> SaveAsync(FloodParcelSoftCostEntity softcost)
    {
        if (softcost.Id > 0)
            return await UpdateAsync(softcost);
        else
            return await CreateAsync(softcost);
    }
    private async Task<FloodParcelSoftCostEntity> CreateAsync(FloodParcelSoftCostEntity softcost)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateSoftCostLineItemsSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = softcost.ApplicationId,
                @p_PamsPin = softcost.PamsPin,
                @p_SoftCostTypeId = softcost.SoftCostTypeId,
                @p_VendorName = softcost.VendorName,
                @p_InvoiceAmount = softcost.InvoiceAmount,
                @p_PaymentAmount = softcost.PaymentAmount,
                @p_LastUpdatedBy = softcost.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        softcost.Id = id;

        return softcost;
    }
    private async Task<FloodParcelSoftCostEntity> UpdateAsync(FloodParcelSoftCostEntity softcost)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateSoftCostLineItemsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = softcost.Id,
                @p_ApplicationId = softcost.ApplicationId,
                @p_SoftCostTypeId = softcost.SoftCostTypeId,
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

