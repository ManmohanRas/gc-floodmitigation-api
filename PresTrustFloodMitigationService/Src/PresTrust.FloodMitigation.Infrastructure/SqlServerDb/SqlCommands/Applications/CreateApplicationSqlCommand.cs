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
               )
            VALUES
               (
                @p_Title
               ,@p_AgencyId
               ,@p_ApplicationTypeId
               ,@p_ApplicationSubTypeId
               ,@p_StatusId
               );
             SELECT CAST(SCOPE_IDENTITY() AS INT);";

    public CreateApplicationSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
