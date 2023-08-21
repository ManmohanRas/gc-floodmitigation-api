namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateOverviewDetailsSqlCommand
{
    private readonly string _sqlCommand =
        @" UPDATE [Flood].[FloodOverview] 
            SET FactorHomes = @p_FactorHomes
                ,FactorContiguousHomes = @p_FactorContiguousHomes
                ,NatlDisaster = @p_NatlDisaster
                ,NatlDisasterId = @p_NatlDisasterId
                ,NatlDisasterName = @p_NatlDisasterName
                ,NatlDisasterYear = @p_NatlDisasterYear
                ,NatlDisasterMonth = @p_NatlDisasterMonth
                ,LOI = @p_LOI
                ,LOIStatus = @p_LOIStatus
                ,LOIApprovedDate = @p_LOIApprovedDate
                ,FEMA_OR_NJDEP_Applied = @p_FEMA_OR_NJDEP_Applied
                ,FEMAApplied = @p_FEMAApplied
                ,FEMAStatus = @p_FEMAStatus
                ,FEMAApprovedDate = @p_FEMAApprovedDate
                ,FEMADenialReason = @p_FEMADenialReason
                ,GreenAcresApplied = @p_GreenAcresApplied
                ,GreenAcresStatus = @p_GreenAcresStatus
                ,GreenAcresApprovedDate = @p_GreenAcresApprovedDate
                ,BlueAcresApplied = @p_BlueAcresApplied
                ,BlueAcresStatus = @p_BlueAcresStatus
                ,BlueAcresApprovedDate = @p_BlueAcresApprovedDate
                ,FundingAgenciesApplied = @p_FundingAgenciesApplied
                ,LastUpdatedBy = @p_LastUpdatedBy
                ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;
        ";

    public UpdateOverviewDetailsSqlCommand()
    {

    }
    public override string ToString()
    {
        return _sqlCommand;
    }

}
