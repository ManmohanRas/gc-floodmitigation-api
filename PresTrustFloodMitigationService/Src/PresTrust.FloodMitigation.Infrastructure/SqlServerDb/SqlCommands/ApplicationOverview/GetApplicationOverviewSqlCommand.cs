namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationOverviewSqlCommand
{
    private readonly String _sqlCommand =
            @" SELECT [Id]
                        ,[ApplicationId]
                        ,[NoOfHomes]
                        ,[NoOfContiguousHomes]
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
    public GetApplicationOverviewSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
