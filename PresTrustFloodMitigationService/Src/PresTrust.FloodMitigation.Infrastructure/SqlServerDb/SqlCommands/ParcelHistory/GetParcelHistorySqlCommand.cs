namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;
public class GetParcelHistorySqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT      [Id],
	                    [CurrentPamsPin],
	                    [PreviousPamsPin],
	                    [ChangeType],
	                    [ReasonForChange],
	                    [LastUpdatedOn]
            FROM        [Flood].[FloodParcelHistory]
            WHERE       [ParcelId] = @p_ParcelId
            ORDER BY    [LastUpdatedOn] DESC;"
       ;
    public GetParcelHistorySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
    
    

