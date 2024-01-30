namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class AnnualFundingAmountsRepository : IAnnualFundingAmountsRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public AnnualFundingAmountsRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<List<FloodAnnualFundingEntity>> GetFundingDetailsAsync()
    {
        List<FloodAnnualFundingEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetFundingDetailsSqlCommand();
        results = (await conn.QueryAsync<FloodAnnualFundingEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds
                       )).ToList();
        return results ?? new();
    }

    public async Task<FloodAnnualFundingEntity> SaveAsync(FloodAnnualFundingEntity details)
    {
        if (details.Id > 0)
            return await UpdateAsync(details);
        else
            return await CreateAsync(details);
    }

    private async Task<FloodAnnualFundingEntity> UpdateAsync(FloodAnnualFundingEntity details)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateFundingAmountsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
               @p_Id                   = details.Id,
               @p_AllocationYear       = DateTime.Now.Year,
               @p_AllocationAmount	   = details.AllocationAmount,
               @p_Interest             = details.Interest,
               @p_AddedOrOmittedAmount = details.AddedOrOmittedAmount,
               @p_Comment              = details.Comment,
               @p_LastUpdatedBy        = details.LastUpdatedBy,
               @p_LastUpdatedOn        = DateTime.Now
            });

        return details;
    }

    private async Task<FloodAnnualFundingEntity> CreateAsync(FloodAnnualFundingEntity details)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateFundingDetailsSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = details.Id,
                @p_AllocationYear = DateTime.Now.Year,
                @p_AllocationAmount = details.AllocationAmount,
                @p_Interest = details.Interest,
                @p_AddedOrOmittedAmount = details.AddedOrOmittedAmount,
                @p_Comment = details.Comment,
                @p_LastUpdatedBy = details.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        details.Id = id;

        return details;
    }
}
