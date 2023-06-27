namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationSqlCommand
{
    private readonly string _sqlCommand =
            @" INSERT INTO [Flood].[FloodApplication]
               (
                [Title]
               ,[AgencyId]
               ,[ApplicationTypeId]
               ,[ApplicationSubTypeId]
               ,[StatusId]
               ,[ExpirationDate]
               ,[CreatedByProgramAdmin]
               ,[LastUpdatedBy]
               ,[LastUpdatedOn]
               )
            VALUES
               (
                @p_Title
               ,@p_AgencyId
               ,@p_ApplicationTypeId
               ,@p_ApplicationSubTypeId
               ,@p_StatusId
               ,DATEADD(YEAR, 3, GETDATE())
               ,@p_CreatedByProgramAdmin
               ,@p_LastUpdatedBy
               ,GetDate()
               );
             SELECT CAST(SCOPE_IDENTITY() AS INT);";

    public CreateApplicationSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
