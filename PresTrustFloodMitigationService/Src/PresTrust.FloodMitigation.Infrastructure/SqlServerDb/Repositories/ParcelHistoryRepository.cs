using static System.Collections.Specialized.BitVector32;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;
public class ParcelHistoryRepository : IParcelHistoryRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ParcelHistoryRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<List<FloodParcelHistoryEntity>> GetParcelHistoryAsync(int parcelId)
    {
        List<FloodParcelHistoryEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelHistorySqlCommand();
        results = (await conn.QueryAsync<FloodParcelHistoryEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ParcelId = parcelId })).ToList();
        return results ?? new();
    }

    public async Task<FloodParcelHistoryEntity> GetParcelHistoryItemAsync(int parcelId)
    {
        FloodParcelHistoryEntity result;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelHistoryItemSqlCommand();
        var results = (await conn.QueryAsync<FloodParcelHistoryEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_ParcelId = parcelId })).ToList();
        
        result = results.FirstOrDefault();
        
        return result ?? new();
    }

    public async Task<FloodParcelHistoryEntity> SaveParcelHistoryAsync(FloodParcelHistoryEntity dialog)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateParcelHistorySqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Section = dialog.Section,
                @p_Partial = dialog.Partial,
                @p_Acres = dialog.Acres,
                @p_AcresToBeAcquired = dialog.AcresToBeAcquired,
                @p_IsThisAnExclusionArea = dialog.IsThisAnExclusionArea,
                @p_Notes = dialog.Notes,
                @p_InterestType = dialog.InterestType,
                @p_EasementId = dialog.EasementId,
                @p_ChangeType = dialog.ChangeType,
                @p_ChangeDate = dialog.ChangeDate,
                @p_ReasonForChange = dialog.ReasonForChange,
                @p_LastUpdatedBy = dialog.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        dialog.Id = id;

        return dialog;
    }
}
