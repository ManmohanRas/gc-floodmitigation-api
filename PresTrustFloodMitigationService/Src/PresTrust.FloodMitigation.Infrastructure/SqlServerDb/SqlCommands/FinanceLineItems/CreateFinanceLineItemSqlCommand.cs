namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateFinanceLineItemSqlCommand
{
    private readonly string _sqlCommand =
            @"  INSERT INTO [Flood].[FloodApplicationFinanceLineItems]
                       ([ApplicationId]
                       ,[PamsPin]
                       ,[Priority]
                       ,[ValueEstimate]
                       ,[LastUpdatedBy]
                       ,[LastUpdatedOn])
                 VALUES
                       (@p_ApplicationId
                       ,@p_PamsPin
                       ,@p_Priority
                       ,@p_ValueEstimate
                       ,@p_LastUpdatedBy
                       ,GETDATE());

                SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateFinanceLineItemSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
