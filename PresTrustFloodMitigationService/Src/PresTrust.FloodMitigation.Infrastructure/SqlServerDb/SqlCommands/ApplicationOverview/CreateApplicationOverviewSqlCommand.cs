namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationOverviewSqlCommand
{
    private readonly string _sqlCommand =
               @"INSERT INTO [Flood].[FloodApplicationOverview]
                    (
                         ApplicationId
                        ,NoOfHomes
                        ,NoOfContiguousHomes
                        ,NatlDisaster
                        ,NatlDisasterId
                        ,NatlDisasterName
                        ,NatlDisasterYear
                        ,NatlDisasterMonth
                        ,LOI
                        ,LOIStatus
                        ,LOIApprovedDate
                        ,FEMA_OR_NJDEP_Applied
                        ,FEMAApplied
                        ,FEMAStatus
                        ,FEMAApprovedDate
                        ,FEMADenialReason
                        ,GreenAcresApplied
                        ,GreenAcresStatus
                        ,GreenAcresApprovedDate
                        ,BlueAcresApplied
                        ,BlueAcresStatus
                        ,BlueAcresApprovedDate
                        ,FundingAgenciesApplied
                        ,LastUpdatedBy
                        ,LastUpdatedOn
)
    VALUES(             
                 @p_ApplicationId
                ,@p_NoOfHomes
                ,@p_NoOfContiguousHomes
                ,@p_NatlDisaster
                ,@p_NatlDisasterId
                ,@p_NatlDisasterName
                ,@p_NatlDisasterYear
                ,@p_NatlDisasterMonth
                ,@p_LOI
                ,@p_LOIStatus
                ,@p_LOIApprovedDate
                ,@p_FEMA_OR_NJDEP_Applied
                ,@p_FEMAApplied
                ,@p_FEMAStatus
                ,@p_FEMAApprovedDate
                ,@p_FEMADenialReason
                ,@p_GreenAcresApplied
                ,@p_GreenAcresStatus
                ,@p_GreenAcresApprovedDate
                ,@p_BlueAcresApplied
                ,@p_BlueAcresStatus
                ,@p_BlueAcresApprovedDate
                ,@p_FundingAgenciesApplied
                ,@p_LastUpdatedBy
               ,GetDate());
 
        SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateApplicationOverviewSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }

}
