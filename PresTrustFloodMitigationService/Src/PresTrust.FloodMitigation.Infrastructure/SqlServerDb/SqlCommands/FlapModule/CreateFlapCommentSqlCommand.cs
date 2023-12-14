namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateFlapCommentSqlCommand
{
    private readonly string _sqlCommand =
        @"  INSERT INTO [Flood].[FloodAgencyFlapComment]
                       ([AgencyId]
                       ,[Comment]
                       ,[LastUpdatedBy]
                       ,[LastUpdatedOn])
                 VALUES
                       (@p_AgencyId
                       ,@p_Comment
                       ,@p_LastUpdatedBy
                       ,GETDATE());

                SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateFlapCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
