namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateFlapSqlCommand
{
    private readonly string _sqlCommand =
        @"  INSERT INTO [Flood].[FloodAgencyFlap]
                       ([AgencyId]
                       ,[FlapApproved]
                       ,[ApprovedDate]
                       ,[LastRevisedDate]
                       ,[FlapMailToGrantee]
                       ,[LastUpdatedBy]
                       ,[LastUpdatedOn])
                 VALUES
                       (@p_AgencyId
                       ,@p_FlapApproved
                       ,@p_ApprovedDate
                       ,@p_LastRevisedDate
                       ,@p_FlapMailToGrantee
                       ,@p_LastUpdatedBy
                       ,GETDATE());

                SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateFlapSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
