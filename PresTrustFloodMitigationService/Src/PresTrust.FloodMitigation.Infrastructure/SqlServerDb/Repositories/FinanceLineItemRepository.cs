namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class FinanceLineItemRepository: IFinanceLineItemRepository
{
    #region " Members ... "

    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    #endregion

    #region " ctor ... "
    public FinanceLineItemRepository(
       PresTrustSqlDbContext context,
       IOptions<SystemParameterConfiguration> systemParamConfigOptions
       )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }
    #endregion

    /// <summary>
    ///  Procedure to fetch all finance line items by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns inance line items.</returns>
    public async Task<IEnumerable<FloodFinanceLineItemEntity>> GetFinanceLineItemsAsync(int applicationId)
    {
        IEnumerable<FloodFinanceLineItemEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFinanceLineItemsByApplicationIdSqlCommand();
        results = await conn.QueryAsync<FloodFinanceLineItemEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });

        return results;
    }

    /// <summary>
    /// Save Finance line item.
    /// </summary>
    /// <param name="financeLineItem"></param>
    /// <returns></returns>
    public async Task<FloodFinanceLineItemEntity> SaveAsync(FloodFinanceLineItemEntity financeLineItem)
    {
        if (financeLineItem.Id > 0)
            return await UpdateAsync(financeLineItem);
        else
            return await CreateAsync(financeLineItem);

    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="financeLineItem"></param>
    /// <returns></returns>
    private async Task<FloodFinanceLineItemEntity> CreateAsync(FloodFinanceLineItemEntity financeLineItem)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFinanceLineItemSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = financeLineItem.ApplicationId,
                @p_PamsPin = financeLineItem.PamsPin,
                @p_Priority = financeLineItem.Priority,
                @p_ValueEstimate = financeLineItem.ValueEstimate,
                @p_LastUpdatedBy = financeLineItem.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        financeLineItem.Id = id;

        return financeLineItem;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="financeLineItem"></param>
    /// <returns></returns>
    private async Task<FloodFinanceLineItemEntity> UpdateAsync(FloodFinanceLineItemEntity financeLineItem)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFinanceLineItemSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = financeLineItem.Id,
                @p_ApplicationId = financeLineItem.ApplicationId,
                @p_PamsPin = financeLineItem.PamsPin,
                @p_Priority = financeLineItem.Priority,
                @p_ValueEstimate = financeLineItem.ValueEstimate,
                @p_LastUpdatedBy = financeLineItem.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return financeLineItem;
    }
}
