using static System.Collections.Specialized.BitVector32;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;
public class ParcelAuditDialogRepository:IParcelAuditDialog
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ParcelAuditDialogRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<List<FloodParcelAuditDialogEntity>> GetParcelAuditDialogAsync(int agencyId)
    {
        List<FloodParcelAuditDialogEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelAuditDialogSqlCommand();
        results = (await conn.QueryAsync<FloodParcelAuditDialogEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_AgencyId = agencyId })).ToList();
        return results ?? new();
    }

    public async Task<FloodParcelAuditDialogEntity> SaveParcelAuditDialogAsync(FloodParcelAuditDialogEntity dialog)
    {
        if (dialog.Id > 0)
            return await UpdateAsync(dialog);
        else
            return await CreateAsync(dialog);
    }

    private async Task<FloodParcelAuditDialogEntity> UpdateAsync(FloodParcelAuditDialogEntity dialog)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateParcelAuditDialogSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = dialog.Id,
                @p_AgencyId = dialog.AgencyId,
                @p_Block = dialog.Block,
                @p_Lot=dialog.Lot,
                @p_QualificationCode = dialog.QualificationCode,
                @p_PamsPin=dialog.PamsPin,
                @p_Section=dialog.Section,
                @p_Partial=dialog.Partial,
                @p_Acres = dialog.Acres,
                @p_AcresToBeAcquired=dialog.AcresToBeAcquired,
                @p_IsThisAnExclusionArea=dialog.IsThisAnExclusionArea,
                @p_Notes=dialog.Notes,
                @p_InterestType=dialog.InterestType,
                @p_EasementId = dialog.EasementId,
                @p_ChangeType = dialog.ChangeType,
                @p_ChangeDate = dialog.ChangeDate,
                @p_PamsPinStatus=dialog.PamsPinStatus,
                @p_ReasonForChange = dialog.ReasonForChange,
                @p_LastUpdatedBy = dialog.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return dialog;
    }
    private async Task<FloodParcelAuditDialogEntity> CreateAsync(FloodParcelAuditDialogEntity dialog)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateParcelAuditDialogSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Section = dialog.Section,
                @p_Partial = dialog.Partial,
                @p_Acres   = dialog.Acres,
                @p_AcresToBeAcquired = dialog.AcresToBeAcquired,
                @p_IsThisAnExclusionArea = dialog.IsThisAnExclusionArea,
                @p_Notes = dialog.Notes,
                @p_InterestType = dialog.InterestType,
                @p_EasementId= dialog.EasementId,
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
   
