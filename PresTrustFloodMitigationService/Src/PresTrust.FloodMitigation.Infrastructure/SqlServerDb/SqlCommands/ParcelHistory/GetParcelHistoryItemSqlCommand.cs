namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;
 public class GetParcelHistoryItemSqlCommand
{
    private readonly string _sqlCommand =
       @"  SELECT TOP 1	[Section],
						[Acres],
						[AcresToBeAcquired],
						[Partial],
						[InterestType],
						[IsThisAnExclusionArea],
						[Notes],
						[EasementId],
						[IsActive],
						[ChangeType],
						[ChangeDate],
						[ReasonForChange]
			FROM		[Flood].[FloodParcelHistory]
			WHERE		[ParcelId] = @p_ParcelId
			ORDER BY	[LastUpdatedOn] DESC;";

    public GetParcelHistoryItemSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
    

