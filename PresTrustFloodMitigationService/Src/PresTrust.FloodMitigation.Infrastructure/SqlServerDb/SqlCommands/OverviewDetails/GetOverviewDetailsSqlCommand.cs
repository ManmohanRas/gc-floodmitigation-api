namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetOverviewDetailsSqlCommand
{
    private readonly String _sqlCommand =
            @" SELECT [Id]
                        ,[ApplicationId]
                        ,[FactorHomes]
                        ,[FactorContiguousHomes]
                        ,[NatlDisaster]
                        ,[NatlDisasterId]
                        ,[NatlDisasterName]
                        ,[NatlDisasterYear]
                        ,[NatlDisasterMonth]
                        ,[LOI]
                        ,[LOIStatus]
                        ,[LOIApprovedDate]
                        ,[FEMA_OR_NJDEP_Applied]
                        ,[FEMAApplied]
                        ,[FEMAStatus]
                        ,[FEMAApprovedDate]
                        ,[FEMADenialReason]
                        ,[GreenAcresApplied]
                        ,[GreenAcresStatus]
                        ,[GreenAcresApprovedDate]
                        ,[BlueAcresApplied]
                        ,[BlueAcresStatus]
                        ,[BlueAcresApprovedDate]
                        ,[FundingAgenciesApplied]
                        ,[LastUpdatedBy]
                        ,[LastUpdatedOn]
                    FROM [Flood].[FloodOverview]
                    WHERE [ApplicationId] = @p_ApplicationId;"
                 ;
    public GetOverviewDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
