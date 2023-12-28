namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateFlapTargetAreaSqlCommand
{
    private readonly string _sqlCommand =
        @"  INSERT INTO [Flood].[FloodFlapTargetArea]
                       ([AgencyId]
                       ,[TargetArea]
                       ,[CreatedDate]
                       ,[LastUpdatedBy]
                       ,[LastUpdatedOn])
                 VALUES
                       (@p_AgencyId
                       ,@p_TargetArea
                       ,@p_CreatedDate
                       ,@p_LastUpdatedBy
                       ,GETDATE());

                SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateFlapTargetAreaSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
