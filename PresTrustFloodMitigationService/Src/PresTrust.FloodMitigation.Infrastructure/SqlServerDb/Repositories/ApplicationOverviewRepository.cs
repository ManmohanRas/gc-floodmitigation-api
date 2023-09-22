namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ApplicationOverviewRepository : IApplicationOverviewRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ApplicationOverviewRepository
       (
       PresTrustSqlDbContext context,
       IOptions<SystemParameterConfiguration> systemParamConfigOptions
       )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<FloodApplicationOverviewEntity> GetOverviewDetailsAsync(int applicationId)
    {
        FloodApplicationOverviewEntity? result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationOverviewSqlCommand();
        var results = await conn.QueryAsync<FloodApplicationOverviewEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ApplicationId = applicationId });

        result = results.FirstOrDefault() ?? new FloodApplicationOverviewEntity();

        return result ?? new FloodApplicationOverviewEntity();
    }

    public async Task<FloodApplicationOverviewEntity> SaveAsync(FloodApplicationOverviewEntity overviewDetails)
    {
        if (overviewDetails.Id > 0)
            return await UpdateAsync(overviewDetails);
        else
            return await CreateAsync(overviewDetails);
    }

    private async Task<FloodApplicationOverviewEntity> UpdateAsync(FloodApplicationOverviewEntity overviewDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationOverviewSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = overviewDetails.Id,
                @p_NoOfHomes = overviewDetails.NoOfHomes,
                @p_NoOfContiguousHomes = overviewDetails.NoOfContiguousHomes,
                @p_ApplicationId = overviewDetails.ApplicationId,
                @p_NatlDisaster = overviewDetails.NatlDisaster,
                @p_NatlDisasterId = overviewDetails.NatlDisasterId,
                @p_NatlDisasterName = overviewDetails.NatlDisasterName,
                @p_NatlDisasterYear = overviewDetails.NatlDisasterYear,
                @p_NatlDisasterMonth = overviewDetails.NatlDisasterMonth,
                @p_LOI = overviewDetails.LOI,
                @p_LOIStatus = overviewDetails.LOIStatus,
                @p_LOIApprovedDate = overviewDetails.LOIApprovedDate,
                @p_FEMA_OR_NJDEP_Applied = overviewDetails.FEMA_OR_NJDEP_Applied,
                @p_FEMAApplied = overviewDetails.FEMAApplied,
                @p_FEMAStatus = overviewDetails.FEMAStatus,
                @p_FEMAApprovedDate = overviewDetails.FEMAApprovedDate,
                @p_FEMADenialReason = overviewDetails.FEMADenialReason,
                @p_GreenAcresApplied = overviewDetails.GreenAcresApplied,
                @p_GreenAcresStatus = overviewDetails.GreenAcresStatus,
                @p_GreenAcresApprovedDate = overviewDetails.GreenAcresApprovedDate,
                @p_BlueAcresApplied = overviewDetails.BlueAcresApplied,
                @p_BlueAcresStatus = overviewDetails.BlueAcresStatus,
                @p_BlueAcresApprovedDate = overviewDetails.BlueAcresApprovedDate,
                @p_FundingAgenciesApplied = overviewDetails.FundingAgenciesApplied,
                @p_LastUpdatedBy = overviewDetails.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return overviewDetails;
    }

    private async Task<FloodApplicationOverviewEntity> CreateAsync(FloodApplicationOverviewEntity overviewDetails)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationOverviewSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_NoOfHomes = overviewDetails.NoOfHomes,
                @p_NoOfContiguousHomes = overviewDetails.NoOfContiguousHomes,
                @p_ApplicationId = overviewDetails.ApplicationId,
                @p_NatlDisaster = overviewDetails.NatlDisaster,
                @p_NatlDisasterId = overviewDetails.NatlDisasterId,
                @p_NatlDisasterName = overviewDetails.NatlDisasterName,
                @p_NatlDisasterYear = overviewDetails.NatlDisasterYear,
                @p_NatlDisasterMonth = overviewDetails.NatlDisasterMonth,
                @p_LOI = overviewDetails.LOI,
                @p_LOIStatus = overviewDetails.LOIStatus,
                @p_LOIApprovedDate = overviewDetails.LOIApprovedDate,
                @p_FEMA_OR_NJDEP_Applied = overviewDetails.FEMA_OR_NJDEP_Applied,
                @p_FEMAApplied = overviewDetails.FEMAApplied,
                @p_FEMAStatus = overviewDetails.FEMAStatus,
                @p_FEMAApprovedDate = overviewDetails.FEMAApprovedDate,
                @p_FEMADenialReason = overviewDetails.FEMADenialReason,
                @p_GreenAcresApplied = overviewDetails.GreenAcresApplied,
                @p_GreenAcresStatus = overviewDetails.GreenAcresStatus,
                @p_GreenAcresApprovedDate = overviewDetails.GreenAcresApprovedDate,
                @p_BlueAcresApplied = overviewDetails.BlueAcresApplied,
                @p_BlueAcresStatus = overviewDetails.BlueAcresStatus,
                @p_BlueAcresApprovedDate = overviewDetails.BlueAcresApprovedDate,
                @p_FundingAgenciesApplied = overviewDetails.FundingAgenciesApplied,
                @p_LastUpdatedBy = overviewDetails.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        overviewDetails.Id = id;

        return overviewDetails;
    }
}
