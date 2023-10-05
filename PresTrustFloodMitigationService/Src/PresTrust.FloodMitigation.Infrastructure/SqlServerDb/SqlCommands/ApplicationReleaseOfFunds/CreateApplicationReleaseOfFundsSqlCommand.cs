namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationReleaseOfFundsSqlCommand
{
    private readonly string _sqlCommand =
        @"  INSERT INTO [Flood].[FloodApplicationPayment]
                       ([ApplicationId]
                       ,[CAFNumber]
                       ,[CAFClosed]
                       ,[LastUpdatedBy]
                       ,[LastUpdatedOn])
                 VALUES
                       (@p_ApplicationId
                       ,@p_CAFNumber
                       ,@p_CAFClosed
                       ,@p_LastUpdatedBy
                       ,GETDATE());

                SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateApplicationReleaseOfFundsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
