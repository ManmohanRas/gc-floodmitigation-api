namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateFinanceSqlCommand
{
    private readonly string _sqlCommand =
            @"  INSERT INTO [Flood].[FloodApplicationFinance]
                       ([ApplicationId]
                       ,[MatchPercent]
                       ,[LastUpdatedBy]
                       ,[LastUpdatedOn])
                 VALUES
                       (@p_ApplicationId
                       ,@p_MatchPercent
                       ,@p_LastUpdatedBy
                       ,GETDATE());

                SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateFinanceSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
